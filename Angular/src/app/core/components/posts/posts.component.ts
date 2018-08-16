import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ForumService} from '../../service/forum.service';

import {IBasicModel} from '../../../shared/model/basic.model';
import {IPostsModel} from './posts.model';
import {PostModel} from '../post/post.model';


@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {

  posts: IPostsModel[];
  categoryId: number;

  constructor(private forumService: ForumService,
              private route: ActivatedRoute,
              private router: Router) {
    this.route.params
      .subscribe((params: { categoryId: string }) => this.categoryId = parseInt(params.categoryId, 10));
  }

  ngOnInit(): void {
    this.loadPosts(this.categoryId);
  }

  private loadPosts(categoryId: number): void {
    this.forumService.getPosts(categoryId)
      .subscribe(data => this.posts = data);
  }

  private loadPost(post: IPostsModel): void {
    this.router.navigate(['forum', this.categoryId, post.id]);
  }

  private addPost(postTitle: string, postBody: string): void {
    const post = new PostModel(postTitle, postBody);
    this.forumService.postPost(this.categoryId, post)
      .subscribe(newComment => this.posts = [...this.posts, newComment]);
  }

  private deletePost(post: IPostsModel): void {
    this.forumService.deletePost(this.categoryId, post.id)
      .subscribe(() => {
        this.posts = this.posts.filter(c => c.id !== post.id);
      });
    event.stopPropagation();
  }

}
