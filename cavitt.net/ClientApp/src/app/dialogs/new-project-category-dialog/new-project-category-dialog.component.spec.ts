import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewProjectCategoryDialogComponent } from './new-project-category-dialog.component';

describe('NewProjectCategoryDialogComponent', () => {
  let component: NewProjectCategoryDialogComponent;
  let fixture: ComponentFixture<NewProjectCategoryDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewProjectCategoryDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewProjectCategoryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
