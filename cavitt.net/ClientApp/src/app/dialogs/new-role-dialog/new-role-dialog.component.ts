import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatInputModule, MatSelectModule } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService, ANONYMOUS_USER, UserService } from '../../services';
import { User, Role } from '../../models';

@Component({
  selector: 'app-new-role-dialog',
  templateUrl: './new-role-dialog.component.html',
  styleUrls: ['./new-role-dialog.component.css']
})

export class NewRoleDialogComponent implements OnInit {

  form: FormGroup;
  newRole: Role;
  constructor(private auth: AuthService,
    private userService: UserService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<NewRoleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) { }

  ngOnInit() {

    this.form = this.fb.group({
      roleName: ['', Validators.required]
    });

  }

  save() {

    this.newRole.name = this.form.controls.roleName.value;

    // Save to db.

    this.dialogRef.close(this.newRole);
  }

  close() {
    this.dialogRef.close();
  }

}
