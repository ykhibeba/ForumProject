import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ForumService} from '../../service/forum.service';
import {PostModel} from './post.model';
import {CommentModel, ICommentModel} from '../comment/comment.model';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {

  categoryId: number;
  postId: number;
  comment: string;
  post = new PostModel();

  constructor(private forumService: ForumService,
              private route: ActivatedRoute) {
    this.route.params
      .subscribe((params: { categoryId: string, postId: string }) => {
        this.categoryId = parseInt(params.categoryId, 10);
        this.postId = parseInt(params.postId, 10);
      });
  }

  ngOnInit() {
    this.loadPost(this.categoryId, this.postId);
  }

  private loadPost(categoryId: number, postId: number): void {
    this.forumService.getPost(categoryId, postId)
      .subscribe(data => this.post = data);
  }

  private addComment(): void {
    const comment = new CommentModel(null, this.comment);
    this.forumService.postComment(this.categoryId, this.postId, comment)
      .subscribe(newComment => {
        this.post.comments = [...this.post.comments, newComment];
        console.log(newComment);
      });
    console.log(this.post.comments);
  }

  saveComment(comment: ICommentModel): void {
    this.forumService.putComment(this.categoryId, this.postId, comment)
      .subscribe(() => {
        this.post.comments.find(c => c.id === comment.id).body = comment.body;
      });
  }

  deleteComment(commentId: number): void {
    this.forumService.deleteComment(this.categoryId, this.postId, commentId)
      .subscribe(() => {
      this.post.comments = this.post.comments.filter(c => c.id !== commentId);
    });
  }

}
