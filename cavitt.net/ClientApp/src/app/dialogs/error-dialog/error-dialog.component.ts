import { Component, OnInit, Inject, Injector} from '@angular/core';
import { ApplicationError } from '../../models';
import { MAT_DIALOG_DATA, MatDialogRef, MatInputModule, MatSelectModule } from '@angular/material';
import * as StackTrace from 'stacktrace-js';

@Component({
  selector: 'app-error-dialog',
  templateUrl: './error-dialog.component.html',
  styleUrls: ['./error-dialog.component.css']
})

export class ErrorDialogComponent implements OnInit {

  error: any;

  constructor(private injector: Injector, private dialogRef: MatDialogRef<ErrorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) error) {
    this.error = error;
  }

  ngOnInit() {
    console.log(this.error)
    
    //const message = error.message ? error.message : error.toString();
  }


  close() {
    this.dialogRef.close();
  }

}
