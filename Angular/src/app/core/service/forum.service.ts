import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

import {ICategoryModel} from '../components/categories/category.model';
import {IPostsModel} from '../components/posts/posts.model';
import {IPostModel} from '../components/post/post.model';
import {ICommentModel} from '../components/comment/comment.model';

@Injectable({providedIn: 'root'})
export class ForumService {

  token = 'Bearer fUfdTQMdK2SC1IglYIPuKu_vZGDXQ5jqP1cfEoZ09d2aXeCWUyFqKI1webj8lHCl8oxya_9s_J_TOcEATD3zHH3xh4854esAVrIZzyezRAh6IloTR0zTVRx7tS5Gs0J_Uk_Nn2OidH7bmb0zYuNlXXSJTVfeVv1SNwhfVA6j3ZUuuZhCTDl-luvZUkXWhmpDAqCdnGjYy5DmQzZBWgicYNYXuf37bMV5anye9dp2PxC0SrMQeIauPLKCNsaZ5UKtQSKu02LZtMf0qDu0pLLMB5iWC4sEKZijhrpnRhns7VrNXBuco4gSNytG2EvnK11P';

  constructor(private http: HttpClient) {
  }

  getCategories(): Observable<ICategoryModel[]> {
    return this.http.get<ICategoryModel[]>('/api/forum');
  }

  getPosts(categoryId: number): Observable<IPostsModel[]> {
    return this.http.get<IPostsModel[]>(`/api/forum/${categoryId}`);
  }

  getPost(categoryId: number, postId: number): Observable<IPostModel> {
    return this.http.get<IPostModel>(`/api/forum/${categoryId}/${postId}`);
  }

  postPost(categoryId: number, post: IPostModel): Observable<IPostModel> {
    const headers = new HttpHeaders().append('Authorization', this.token)
      .append('Content-Type', 'application/json; charset=utf-8');
    return this.http.post<IPostModel>(`/api/forum/${categoryId}/post`, post, {headers});
  }

  deletePost(categoryId: number, postId: number): Observable<any> {
    const headers = new HttpHeaders().append('Authorization', this.token)
      .append('Content-Type', 'application/json; charset=utf-8');
    return this.http.delete(`/api/forum/${categoryId}/${postId}/delete`, {headers});
  }

  postComment(categoryId: number, postId: number, comment: ICommentModel): Observable<ICommentModel> {
    const headers = new HttpHeaders().append('Authorization', this.token)
      .append('Content-Type', 'application/json; charset=utf-8');
    return this.http.post<ICommentModel>(`/api/forum/${categoryId}/${postId}/comments`, comment, {headers});
  }

  putComment(categoryId: number, postId: number, comment: ICommentModel): Observable<ICommentModel> {
    const headers = new HttpHeaders().append('Authorization', this.token)
      .append('Content-Type', 'application/json; charset=utf-8');
    return this.http.put<ICommentModel>(`/api/forum/${categoryId}/${postId}/${comment.id}`, comment, {headers});
  }

  deleteComment(categoryId: number, postId: number, commentId: number): Observable<any> {
    const headers = new HttpHeaders().append('Authorization', this.token)
      .append('Content-Type', 'application/json; charset=utf-8');
    return this.http.delete(`/api/forum/${categoryId}/${postId}/${commentId}/delete`, {headers});
  }
}
