import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCommentDailogComponent } from './new-comment-dailog.component';

describe('NewCommentDailogComponent', () => {
  let component: NewCommentDailogComponent;
  let fixture: ComponentFixture<NewCommentDailogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewCommentDailogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCommentDailogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
