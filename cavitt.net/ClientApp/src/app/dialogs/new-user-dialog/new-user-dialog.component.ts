import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatInputModule, MatSelectModule } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService, ANONYMOUS_USER, UserService } from '../../services';
import { User, Role } from '../../models';

@Component({
  selector: 'app-new-user-dialog',
  templateUrl: './new-user-dialog.component.html',
  styleUrls: ['./new-user-dialog.component.css']
})
export class NewUserDialogComponent implements OnInit {

  user: User = ANONYMOUS_USER;
  roles: Role[] = [];
  constructor(private auth: AuthService,
    private userService: UserService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<NewUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {

    // this.description = data.description;
  }

  form: FormGroup;
  description: string;

  ngOnInit() {

    // load roles for dropdown.
    this.userService.getRoles().subscribe((roles: Role[]) => { this.processRoles(roles) });

    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      rolesList: [this.roles]
    });

    this.form.patchValue({
      'role': 'Author'
    });
  }

  processRoles(roles: Role[]) {
    console.log("roles: " + roles);
    this.roles = roles;
  }

  save() {

    this.user.email = this.form.controls.email.value;
    this.user.firstName = this.form.controls.firstName.value;
    this.user.lastName = this.form.controls.lastName.value;
    this.user.password = this.form.controls.password.value;
    this.dialogRef.close(this.user);
  }

  close() {
    this.dialogRef.close();
  }

  onSelect(event) {

  }

}
