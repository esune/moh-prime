import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentCertificateComponent } from './enrolment-certificate.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('EnrolmentCertificateComponent', () => {
  let component: EnrolmentCertificateComponent;
  let fixture: ComponentFixture<EnrolmentCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        NgxMaterialModule,
        NgxProgressModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      declarations: [
        EnrolmentCertificateComponent,
        HeaderComponent,
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
