import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {UserComponent} from './core/components/user/user.component';
import {PageNotFoundComponent} from './core/components/page-not-found/page-not-found.component';
import {CategoriesComponent} from './core/components/categories/categories.component';
import {PostsComponent} from './core/components/posts/posts.component';
import {PostComponent} from './core/components/post/post.component';

const routes: Routes = [
  {path: '', redirectTo: 'forum', pathMatch: 'full'},
  {path: 'forum', component: CategoriesComponent},
  {path: 'forum/:categoryId', component: PostsComponent},
  {path: 'forum/:categoryId/:postId', component: PostComponent},
  {path: 'user', component: UserComponent},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
