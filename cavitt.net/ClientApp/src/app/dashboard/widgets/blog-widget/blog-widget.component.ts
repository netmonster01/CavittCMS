import { Component, OnInit } from '@angular/core';
import { BlogService, AuthService, ANONYMOUS_USER, VoteService } from '../../../services';
import { Post, User, Comment } from '../../../models';
import { Observable } from 'rxjs/Observable';


@Component({
  selector: 'app-blog-widget',
  templateUrl: './blog-widget.component.html',
  styleUrls: ['./blog-widget.component.css']
})

export class BlogWidgetComponent implements OnInit {

  user: User = ANONYMOUS_USER;
  post: Post;
  isLoggedIn = false;

  constructor(private blog: BlogService, private auth: AuthService, private voter: VoteService) { }

  ngOnInit() {

    this.post = new Post();
    this.activate();
  }

  activate() {

    this.blog.getLatestPost().subscribe((post: Post) => this.processData(post));

  }

  processUserData(data: User): void {

    this.user = data;
    if (this.user && this.user.id != null) {
      //this.myVote.userId = data.id;
    }

  }

  processData(post: Post): void {

    this.isLoggedIn = this.auth.isUserLoggedIn();

    this.auth.user$.subscribe(data => this.processUserData(data));

    if (this.isLoggedIn) {

      this.user = this.auth.loggedInUser();
    }

    //console.log(post);

    this.post = post;
  }
}
