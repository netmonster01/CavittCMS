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
      this.raspberryPi = this.projects[0];
      this.raspberryPi.fullThumbnailImageSrc = this.raspberryPi.thumbnailImageType + this.raspberryPi.thumbnailImage;

      this.angular = this.projects[1];
      this.angular.fullThumbnailImageSrc = this.angular.thumbnailImageType + this.angular.thumbnailImage;
    }
  }
}
