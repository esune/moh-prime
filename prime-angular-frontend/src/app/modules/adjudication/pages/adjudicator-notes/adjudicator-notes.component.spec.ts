import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { AdjudicatorNotesComponent } from './adjudicator-notes.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

describe('AdjudicatorNotesComponent', () => {
  let component: AdjudicatorNotesComponent;
  let fixture: ComponentFixture<AdjudicatorNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          ReactiveFormsModule,
          RouterTestingModule,
          AdjudicationModule
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: ConfigService,
            useValue: MockConfigService
          },
          {
            provide: AuthenticationService,
            useClass: MockAuthenticationService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicatorNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
