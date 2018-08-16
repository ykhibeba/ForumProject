import {Component, Input, Output, EventEmitter, OnChanges, SimpleChanges} from '@angular/core';
import {CommentModel, ICommentModel} from './comment.model';

type CommentComponentModel = ICommentModel & { edit: boolean };

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnChanges {

  @Input() comments: CommentComponentModel[];
  @Output() save = new EventEmitter<ICommentModel>();
  @Output() delete = new EventEmitter<number>();

  ngOnChanges(changes: SimpleChanges): void {
    if ('comments' in changes && this.comments) {
      this.comments.filter(v => !('edit' in v))
        .forEach(v => v['edit'] = false);
    }
  }

  onSave(comment: CommentComponentModel, newValue: string): void {
    comment.edit = false;
    this.save.emit(new CommentModel(comment.id, newValue));
  }

  onDeleteComment(comment: CommentComponentModel): void {
    this.delete.emit(comment.id);
  }

}
