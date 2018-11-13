import { Component, OnInit } from '@angular/core';
import { Role } from '../../models/role';
import { UserService } from '../../services/user.service';
import { NewRoleDialogComponent } from 'src/app/dialogs';
import { MatDialogConfig, MatDialog } from '@angular/material';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  constructor(private _usersService: UserService, private dialog: MatDialog) { }
  Roles: Role[] = [];

  ngOnInit() {
    this.getRoles();
  }

  getRoles() {
    this._usersService.getRoles().subscribe((data: Role[]) => this.popData(data));
  }

  popData(roles: Role[]) {
    console.log(roles);
    this.Roles = roles;
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

    const dialogRef = this.dialog.open(NewRoleDialogComponent, { width: '300px', hasBackdrop: false });

    dialogRef.afterClosed().subscribe(
      data => console.log('Dialog output:', data)
    );
  }
}
