import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../services';
import { Project } from '../models/project';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  constructor(private projectService: ProjectService  ) { }
  projects: Project[] = [];
  raspberryPi: Project;
  angular: Project;

  ngOnInit() {
    this.getProjects();
  }

  getProjects() {
    this.projectService.getProjects().subscribe((data: Project[]) => this.processData(data));
  }

  processData(projects: Project[]) {
    if (projects) {
      this.projects = projects;
    }
  }
}
