import { Component, OnInit } from '@angular/core';
import { BlogService } from '../../services';
import { Blog, Post } from '../../models';
import { MatDialog, MatDialogConfig } from "@angular/material";
import { NewBlogDialogComponent } from '../../dialogs';
import { EditPostDialogComponent } from '../../dialogs';

@Component({
  selector: 'app-admin-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})

export class BlogComponent implements OnInit {

  constructor(private blog: BlogService, private dialog: MatDialog) { }

  posts: Post[] = [];

  ngOnInit() {
    this.loadPosts();
  }

  loadPosts() {
    this.blog.getPosts().subscribe((posts: Post[]) => this.processPosts(posts));
  }

  processPosts(posts: Post[]): void {
    this.posts = posts;
  }

  openDialog() {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      id: 1,
      hasBackdrop: false,
      width: '500px'
    };

    const dialogRef = this.dialog.open(NewBlogDialogComponent, { width: '300px', hasBackdrop: false });

    dialogRef.afterClosed().subscribe(
      data => console.log('Dialog output:', data)
    );
  }

  openEditDialog(post: Post) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(EditPostDialogComponent, { width: '300px', hasBackdrop: false, data: post });

    dialogRef.afterClosed().subscribe(
      data => console.log('Dialog output:', data)
    );
  }

}
