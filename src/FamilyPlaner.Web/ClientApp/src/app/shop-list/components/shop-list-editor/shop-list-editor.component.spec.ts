import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopListEditorComponent } from './shop-list-editor.component';

describe('ShopListEditorComponent', () => {
  let component: ShopListEditorComponent;
  let fixture: ComponentFixture<ShopListEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShopListEditorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShopListEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
