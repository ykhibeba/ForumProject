import {ICommentModel} from '../comment/comment.model';

export interface IPostModel extends ICommentModel {
  title: string;
  comments: ICommentModel[];
}

export class PostModel implements IPostModel {
  id: number;
  title: string;
  datetime: string;
  name: string;
  username: string;
  body: string;
  comments: ICommentModel[];

  constructor(title?: string, body?: string) {
    this.title = title;
    this.body = body;
  }
}

