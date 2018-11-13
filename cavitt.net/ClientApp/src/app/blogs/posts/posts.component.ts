import { Component, OnInit } from '@angular/core';
import { BlogService, AuthService, ANONYMOUS_USER } from '../../services';
import { Post, User } from '../../models';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})

export class PostsComponent implements OnInit {

  posts: Post[] = [];
  user: User = ANONYMOUS_USER;

  constructor(private blog: BlogService, private auth: AuthService) { }

  ngOnInit() {
  this.loadPosts();
  this.auth.user$.subscribe(data => this.processUserData(data));
  }

  processUserData(data: User): void {
    this.user = data;
  }

  loadPosts() {
    this.blog.getPosts().subscribe((posts: Post[]) => this.processPosts(posts));
  }

  processPosts(posts: Post[]): void {
    this.posts = posts;
  }
}
