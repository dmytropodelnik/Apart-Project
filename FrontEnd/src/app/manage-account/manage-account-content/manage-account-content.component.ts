import {
  Input,
  Component,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from 'src/app/services/main-data.service';

@Component({
  selector: 'app-manage-account-content',
  templateUrl: './manage-account-content.component.html',
  styleUrls: ['./manage-account-content.component.css'],
})
export class ManageAccountContentComponent implements OnInit {
  @Input() content: string | undefined;
  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;

  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.content = 'personal-details';
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  setContent(newContent: string) {
    this.content = newContent;
  }

  onChangedEmail(setting: string) {
    this.content = setting;
  }

  ngOnInit(): void {}
}
