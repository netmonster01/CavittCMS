import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatInputModule, MatSelectModule } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService, ANONYMOUS_USER, UserService, BlogService } from '../../services';
import { User, Post } from '../../models';

@Component({
  selector: 'app-new-blog-dialog',
  templateUrl: './new-blog-dialog.component.html',
  styleUrls: ['./new-blog-dialog.component.css']
})
export class NewBlogDialogComponent implements OnInit {
  form: FormGroup;
  user: User = ANONYMOUS_USER;
  newPost: Post;
  isLoggedIn = false;
  preview = false;
  previewContent: string;

  constructor(private auth: AuthService,
    private userService: UserService,
    private blogService: BlogService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<NewBlogDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) { }

  ngOnInit() {

    this.isLoggedIn = this.auth.isUserLoggedIn();
    if (this.isLoggedIn) {
      this.user = this.auth.loggedInUser();
    }

    this.form = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required]
    });

  }

  close() {
    this.dialogRef.close();
  }

  closeWithState(state: boolean) {
    this.dialogRef.close();
  }

  save() {

    this.newPost = new Post();
    this.newPost.content = this.form.controls.content.value;
    this.newPost.title = this.form.controls.title.value;
    this.newPost.userId = this.user.id;
    //this.newPost.author = this.user.firstName + this.user.lastName;
    this.blogService.createPost(this.newPost).subscribe((didCreate: boolean) => this.processNewPost(didCreate));
  }

  processNewPost(didCreate: boolean): void {

    this.closeWithState(didCreate);
  }

  showPreview(event: any) {

    this.preview = event.currentTarget.checked;
    this.previewContent = this.form.controls.content.value;
    console.log(event.currentTarget.checked);
    event.stopPropagation();
    return false;
  }

  updatePreview() {
    this.previewContent = this.form.controls.content.value;
  }
}
