import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {LayoutModule} from '@angular/cdk/layout';
import {MatInputModule, MatFormFieldModule} from '@angular/material';
import {MatToolbarModule, MatSidenavModule, MatIconModule, MatListModule, MatButtonModule, MatCardModule, MatTreeModule, MatTabsModule} from '@angular/material';

import {AppComponent} from './app.component';
import {AppRoutingModule} from './app.router';
import {CategoriesComponent} from './core/components/categories/categories.component';
import {PostsComponent} from './core/components/posts/posts.component';
import {PostComponent} from './core/components/post/post.component';
import {CommentComponent} from './core/components/comment/comment.component';
import {UserComponent} from './core/components/user/user.component';
import {PageNotFoundComponent} from './core/components/page-not-found/page-not-found.component';
import { TreeComponent } from './core/components/tree/tree.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoriesComponent,
    PostComponent,
    UserComponent,
    PageNotFoundComponent,
    PostsComponent,
    CommentComponent,
    TreeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatTreeModule,
    MatTabsModule,
    MatFormFieldModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
