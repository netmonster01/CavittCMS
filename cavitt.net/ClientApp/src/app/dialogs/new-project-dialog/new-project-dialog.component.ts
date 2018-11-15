import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { Project, ProjectCategory } from '../../models';
import { ProjectService } from '../../services';

@Component({
  selector: 'app-new-project-dialog',
  templateUrl: './new-project-dialog.component.html',
  styleUrls: ['./new-project-dialog.component.css']
})
export class NewProjectDialogComponent implements OnInit {

  form: FormGroup;
  newProject: Project;
  categories: ProjectCategory [] = [];

  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<NewProjectDialogComponent>, private projectService: ProjectService ) { }

  ngOnInit() {

    this.form = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
      gitHubUrl: [''],
      catogory: ['', Validators.required],
    });

    this.getCategories();
  }

  getCategories(): any {
    this.projectService.getProjectCategories().subscribe((categories: ProjectCategory[]) => this.processCategories(categories));
  }

  processCategories(categories: ProjectCategory[]): void {
    if (categories) {
      this.categories = categories;
    }
  }

  save() {
   // this.newProject.title = this.form.controls.roleName.value;
    this.dialogRef.close(this.newProject);
  }

  close() {
    this.dialogRef.close();
  }
}
