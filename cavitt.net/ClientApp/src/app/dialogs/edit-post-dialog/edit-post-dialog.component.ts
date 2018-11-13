import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatInputModule, MatSelectModule } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService, ANONYMOUS_USER, UserService } from '../../services';
import {  Post } from '../../models';

@Component({
  selector: 'app-edit-post-dialog',
  templateUrl: './edit-post-dialog.component.html',
  styleUrls: ['./edit-post-dialog.component.css']
})
export class EditPostDialogComponent implements OnInit {

  post: Post;
  form: FormGroup;

  constructor(private auth: AuthService,
    private userService: UserService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<
      EditPostDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    this.post = data;
    }

  ngOnInit() {

    this.form = this.fb.group({
      title: [this.post.title, Validators.required],
      content: [this.post.content, Validators.required]
    });

  }

  close() {
    this.dialogRef.close();
  }

}
