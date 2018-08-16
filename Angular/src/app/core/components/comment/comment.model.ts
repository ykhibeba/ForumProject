import {IBasicModel} from '../../../shared/model/basic.model';

export interface ICommentModel extends IBasicModel {
  datetime: string;
  name: string;
  username: string;
  body: string;
}

export class CommentModel implements ICommentModel {
  id: number;
  datetime: string;
  name: string;
  username: string;
  body: string;

  constructor(id: number, body: string) {
    this.id = id;
    this.body = body;
  }
}
