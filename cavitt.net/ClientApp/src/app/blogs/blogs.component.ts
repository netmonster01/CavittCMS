import { Component, OnInit } from '@angular/core';
import { BlogService, AuthService, ANONYMOUS_USER } from '../services';
import { Blog, User } from '../models';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { NewBlogDialogComponent } from '../dialogs';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css']
})

export class BlogsComponent implements OnInit {

  constructor(private _blogService: BlogService, private auth: AuthService, private dialog: MatDialog) { }

  blog: Blog;
  user: User = ANONYMOUS_USER;
  isLoggedIn = false;
  ngOnInit() {
    this.activate();
  }
    activate(): void {

      this.isLoggedIn = this.auth.isUserLoggedIn();
      if (this.isLoggedIn) {
        this.user = this.auth.loggedInUser();
      }
      this.getBlog();

  }

  getBlog() {
    this._blogService.getBlogs().subscribe((data: Blog) => this.popData(data));
  }

  popData(blog: Blog) {
    console.log(blog);
    this.blog =  blog;
  }

  openDialog() {

    //const dialogConfig = new MatDialogConfig();

    //dialogConfig.disableClose = true;
    //dialogConfig.autoFocus = true;


    const dialogRef = this.dialog.open(NewBlogDialogComponent, { hasBackdrop: false });

    dialogRef.afterClosed().subscribe(
      data => console.log('Dialog output:', data)
    );
  }
}
