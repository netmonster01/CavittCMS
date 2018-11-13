import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatInputModule, MatSelectModule } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Comment, User, Post } from '../../models';
import { AuthService, ANONYMOUS_USER, BlogService } from '../../services';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-new-comment-dailog',
  templateUrl: './new-comment-dailog.component.html',
  styleUrls: ['./new-comment-dailog.component.css']
})
export class NewCommentDailogComponent implements OnInit {

  post: Post;
  form: FormGroup;
  comment: Comment = new Comment();
  user: User;
  isLoggedIn = false;
  isLoggedOut = false;

  constructor(private auth: AuthService,
    private blog: BlogService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<NewCommentDailogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {

    this.post = data;
  }

  ngOnInit() {

    this.isLoggedIn = this.auth.isUserLoggedIn();

    this.auth.user$.subscribe(data => this.processData(data));
    this.auth.isLoggedIn$.subscribe(isLoggedIn => this.isLoggedIn === isLoggedIn);
    this.auth.isLoggedOut$.subscribe(isLoggedOut => this.isLoggedOut === isLoggedOut);

    if (this.isLoggedIn) {
      this.user = this.auth.loggedInUser();
    }
    this.form = this.fb.group({
      message: ['', Validators.required]
    });
  }

  processData(data: User): void {
    this.user = data;
    //if (this.user != null && this.user.id != null)
    //  this.isLoggedIn =  
   }

  close() {
    this.dialogRef.close();
  }

  closewithData () {
    this.dialogRef.close(this.comment);
  }
  save() {

    // build up comment object
    this.comment.userId = this.user.id;
    this.comment.message = this.form.controls.message.value;
    this.comment.postId = this.post.postId;
    this.comment.userName = this.user.email;

    // update db.
    this.blog.createComment(this.comment).subscribe((data: boolean) => this.processCommentCreated(data));
  }

  processCommentCreated(data: boolean): void {
    if (data) {
      this.closewithData();
    }
    else {
      close();
    }
  }
}
