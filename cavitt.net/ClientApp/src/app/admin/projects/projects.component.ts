import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../services';
import { Project, ProjectCategory } from '../../models';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { NewProjectDialogComponent } from '../../dialogs';

@Component({
  selector: 'app-admin-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class AdminProjectsComponent implements OnInit {

  newProject: Project;
  projects: Project[] = [];
  constructor(private projectService: ProjectService, private dialog: MatDialog) { }

  ngOnInit() {
    this.getProjects();
  }

  getProjects(): any {
    this.projectService.getProjects().subscribe((projects: Project[]) => this.processProjects(projects));
  }

  processProjects(projects: Project[]): void {
    if (projects) {
      this.projects = projects;
    }
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

    const dialogRef = this.dialog.open(NewProjectDialogComponent, { width: '400px', hasBackdrop: false });

    dialogRef.afterClosed().subscribe(
      data => console.log('Dialog output:', data)
    );
  }

}
