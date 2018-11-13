import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoRolesComponent } from './no-roles.component';

describe('NoRolesComponent', () => {
  let component: NoRolesComponent;
  let fixture: ComponentFixture<NoRolesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoRolesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoRolesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
