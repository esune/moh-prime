import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudicator-actions',
  templateUrl: './adjudicator-actions.component.html',
  styleUrls: ['./adjudicator-actions.component.scss']
})
export class AdjudicatorActionsComponent implements OnInit {
  @Input() public enrollee: HttpEnrollee;
  @Output() public approve: EventEmitter<HttpEnrollee>;
  @Output() public decline: EventEmitter<number>;
  @Output() public lock: EventEmitter<number>;
  @Output() public unlock: EventEmitter<number>;
  @Output() public enableEnrollee: EventEmitter<number>;
  @Output() public toggleManualAdj: EventEmitter<HttpEnrollee>;
  @Output() public enableEditing: EventEmitter<number>;
  @Output() public rerunRules: EventEmitter<number>;
  @Output() public delete: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public EnrolmentStatus = EnrolmentStatus;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private authService: AuthService
  ) {
    this.approve = new EventEmitter<HttpEnrollee>();
    this.decline = new EventEmitter<number>();
    this.lock = new EventEmitter<number>();
    this.unlock = new EventEmitter<number>();
    this.enableEnrollee = new EventEmitter<number>();
    this.enableEditing = new EventEmitter<number>();
    this.rerunRules = new EventEmitter<number>();
    this.delete = new EventEmitter<number>();
    this.toggleManualAdj = new EventEmitter<HttpEnrollee>();
    this.route = new EventEmitter<string | (string | number)[]>();
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public get canDelete(): boolean {
    return this.authService.isSuperAdmin();
  }

  public get isUnderReview(): boolean {
    return (this.enrollee && this.enrollee.currentStatus.statusCode === EnrolmentStatus.UNDER_REVIEW);
  }

  public onApprove() {
    // TODO more consistent to pass enrollee ID and find them on the other side
    if (this.canEdit && this.isUnderReview) {
      this.approve.emit(this.enrollee);
    }
  }

  public onDecline() {
    if (this.canEdit && this.isUnderReview) {
      this.decline.emit(this.enrollee.id);
    }
  }

  public onLock() {
    if (this.canEdit) {
      this.lock.emit(this.enrollee.id);
    }
  }

  public onUnlock() {
    if (this.canEdit) {
      this.unlock.emit(this.enrollee.id);
    }
  }

  public onDelete() {
    if (this.canDelete) {
      this.delete.emit(this.enrollee.id);
    }
  }

  public onEnableEnrollee() {
    if (this.canEdit) {
      this.enableEnrollee.emit(this.enrollee.id);
    }
  }

  public onEnableEditing() {
    if (this.canEdit) {
      this.enableEditing.emit(this.enrollee.id);
    }
  }

  public onRerunRules() {
    if (this.canEdit) {
      this.rerunRules.emit(this.enrollee.id);
    }
  }

  public onToggleManualAdj() {
    if (this.canEdit) {
      this.toggleManualAdj.emit(this.enrollee);
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public ngOnInit() { }
}
