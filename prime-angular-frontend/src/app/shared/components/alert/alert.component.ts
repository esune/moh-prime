import {
  Component,
  OnInit,
  Input,
  ContentChildren,
  QueryList,
  AfterContentInit,
  TemplateRef
} from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent {
  @Input() type: string;
  @Input() icon: string;

  @ContentChildren('alertTitle', { descendants: true })
  public alertTitleChildren: QueryList<TemplateRef<any>>;
  @ContentChildren('alertContent', { descendants: true })
  public alertContentChildren: QueryList<TemplateRef<any>>;

  constructor() {
    this.type = 'info';
    this.icon = 'info';
  }

  public hasTitle(): boolean {
    return !!this.alertTitleChildren.length || false;
  }

  public getType(): string {
    return `alert-${this.type}`;
  }
}
