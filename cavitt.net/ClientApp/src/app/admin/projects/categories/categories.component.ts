import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services';
import { Project, ProjectCategory } from '../../../models';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { NewProjectCategoryDialogComponent } from 'src/app/dialogs/new-project-category-dialog/new-project-category-dialog.component';

@Component({
  selector: 'app-admin-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class AdminCategoriesComponent implements OnInit {

  categories: ProjectCategory[] = [];
  constructor(private projectService: ProjectService, private dialog: MatDialog) { }

  ngOnInit() {
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

  openDialog() {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      id: 1,
      hasBackdrop: false,
      width: '500px'
    };

    const dialogRef = this.dialog.open(NewProjectCategoryDialogComponent, { width: '400px', hasBackdrop: false });

    dialogRef.afterClosed().subscribe(
      data => console.log('Dialog output:', data)
    );
  }
}
