import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { EnrolmentComponent } from './enrolment.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { EnrolmentPipe } from '@shared/pipes/enrolment.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { EnrolleeReviewComponent } from '@shared/components/enrollee/enrollee-review/enrollee-review.component';
import { EnrolleeProfileComponent } from '@shared/components/enrollee/enrollee-profile/enrollee-profile.component';
import { EnrolleeAddressComponent } from '@shared/components/enrollee/enrollee-address/enrollee-address.component';
import { EnrolleeOrganizationsComponent } from '@shared/components/enrollee/enrollee-organizations/enrollee-organizations.component';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';
import { PageComponent } from '@shared/components/page/page.component';
import { EnrolleePropertyErrorComponent } from '@shared/components/enrollee/enrollee-property-error/enrollee-property-error.component';

describe('EnrolmentComponent', () => {
  let component: EnrolmentComponent;
  let fixture: ComponentFixture<EnrolmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          RouterTestingModule
        ],
        declarations: [
          EnrolmentComponent,
          PageComponent,
          PageHeaderComponent,
          PageSubheaderComponent,
          EnrolleeReviewComponent,
          EnrolleeProfileComponent,
          EnrolleeAddressComponent,
          EnrolleeOrganizationsComponent,
          EnrolleePropertyComponent,
          ConfigCodePipe,
          EnrolmentPipe,
          FormatDatePipe,
          PhonePipe,
          PostalPipe,
          DefaultPipe,
          EnrolleePipe,
          YesNoPipe,
          EnrolleePropertyErrorComponent
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
