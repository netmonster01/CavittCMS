import { Component, OnInit, Input, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Project } from '../../models';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  currentProject: Project;

  @Input() set project(project: Project) {

    if (project) {
      this.currentProject = project;
    }
  }
  constructor(@Inject(DOCUMENT) private document: any) { }

  ngOnInit() {
  }

  openGitHubUrl() {

    this.document.location.href = this.currentProject.gitHubUrl;
  }

}
