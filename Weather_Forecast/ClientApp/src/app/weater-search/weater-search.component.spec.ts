import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeaterSearchComponent } from './weater-search.component';

describe('WeaterSearchComponent', () => {
  let component: WeaterSearchComponent;
  let fixture: ComponentFixture<WeaterSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeaterSearchComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeaterSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
