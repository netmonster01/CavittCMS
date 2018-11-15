import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ProjectCategory } from '../../models';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ProjectService } from '../../services';

@Component({
  selector: 'app-new-project-category-dialog',
  templateUrl: './new-project-category-dialog.component.html',
  styleUrls: ['./new-project-category-dialog.component.css']
})
export class NewProjectCategoryDialogComponent implements OnInit {
  form: FormGroup;
  newProjectCategory: ProjectCategory;
  selectedFile: File;
  imageSrc = '/assets/images/uploadIcon.png';

  constructor(private projectService: ProjectService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<NewProjectCategoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) { }

  ngOnInit() {
  }

  save() {

    if (this.newProjectCategory.categoryName && this.newProjectCategory.categoryDescription) {
      this.newProjectCategory.categoryName = this.form.controls.categoryName.value;
      this.newProjectCategory.categoryDescription = this.form.controls.categoryDescription.value;

      // Save to db.
      this.projectService.createProjectCategory(this.newProjectCategory).subscribe((didAdd: boolean) => this.processAddCategory(didAdd));
     
    }
    else {
      this.close();
    }
  }

  processAddCategory(didAdd: boolean): void {
    if (didAdd) {
      this.dialogRef.close(this.newProjectCategory);
    }
  }

  close() {
    this.dialogRef.close();
  }

  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
    // setup reader to read input.
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.imageSrc = e.target.result;
      this.newProjectCategory.thumbnail = e.target.result.split('base64,')[1];
      //this.user.avatarImageType = e.target.result.split(',')[0] + ',';
    };

    reader.readAsDataURL(this.selectedFile);
  }
}
