import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProjectCategory } from '../../models/';
import { ProjectService } from '../../services';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  @Output() showPanel = new EventEmitter<number>();

  constructor(private projectService: ProjectService) {
    this.showPanel.emit(0);
  }

  projectCategories: ProjectCategory[] = [];
  ngOnInit() {

    this.getCategories();
  }

  getCategories() {
    this.projectService.getProjectCategories().subscribe((projectCategories: ProjectCategory[]) => this.processData(projectCategories));
  }

  processData(projectCategories: ProjectCategory[]): void {
    if (projectCategories) {
      this.projectCategories = projectCategories;
    }

  }

  viewProjects(type: number) {
    if (type > 0) {
      console.log("emited:" + type);
      this.showPanel.emit(type);
    }
  }
}
