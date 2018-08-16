import {IBasicModel} from '../../../shared/model/basic.model';

export interface IPostsModel extends IBasicModel {
  title: string;
  name: string;
  username: string;
  datetime: string;
}
