import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewBlogDialogComponent } from './new-blog-dialog.component';

describe('NewBlogDialogComponent', () => {
  let component: NewBlogDialogComponent;
  let fixture: ComponentFixture<NewBlogDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewBlogDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewBlogDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
