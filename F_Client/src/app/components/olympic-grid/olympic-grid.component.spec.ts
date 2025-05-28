import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OlympicGridComponent } from './olympic-grid.component';

describe('OlympicGridComponent', () => {
  let component: OlympicGridComponent;
  let fixture: ComponentFixture<OlympicGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OlympicGridComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OlympicGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
