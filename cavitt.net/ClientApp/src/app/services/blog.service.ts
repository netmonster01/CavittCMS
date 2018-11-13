import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Blog, Post, BlogImage, Comment } from '../models';
import { Observable } from 'rxjs/Observable';
import { concat } from 'rxjs';

export const POST_PLACEHOLDER: Post = {
  postId: 0,
  userId: null,
  author: null,
  comments: [],
  content: null,
  disLikes: 0,
  likes: 0,
  title: null,
  blogId: null,
  dateCreated: null,
  dateModified: null,
  
};

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private _http: HttpClient) { }
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

  getBlogs() {

    return this._http.get<Post[]>('/api/Blogs')
      .catch(this.handleError);
  }

  getLatestPost() {

    return this._http.get<Post>('/api/Blogs/LatestPost')
      .catch(this.handleError);
  }

  getPosts() {

    return this._http.get<Post[]>('/api/Blogs/Posts')
      .catch(this.handleError);
  }

  handleError(handleError: any): any {
    console.log(handleError);
  }

  uploadIamge(blogFile: FormData) {

    let options = {
      headers: this.headers
    };

    this._http.post('/api/FileUpload', blogFile).subscribe(result => { console.log(result) });
  }

  createComment(comment: Comment) {

    let options = {
      headers: this.headers
    };
    return this._http.post<boolean>('/api/Blogs/Comment', comment, options).catch(this.handleError);//.subscribe(didUpdate => console.log(didUpdate)).catch(this.handleError);
  }

  deletePost(postId: number): Observable<{}> {
    let options = {
      headers: this.headers
    };
 
    //const url = `${this.heroesUrl}/${id}`; // DELETE api/heroes/42
    return this._http.delete('/api/Blogs/Post/' + postId, options)
      .catch(this.handleError);
  }

  createPost(post: Post) {

    let options = {
      headers: this.headers
    };

    return this._http.post<Post>('/api/Blogs/Post', post, options).catch(this.handleError);
  }

}
