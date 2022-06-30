import { ThisReceiver } from '@angular/compiler';
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
  selector: 'app-admin-content',
  templateUrl: './admin-content.component.html',
  styleUrls: ['./admin-content.component.css'],
})
export class AdminContentComponent implements OnInit {
  @Input() content: string | undefined;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.content = 'users';
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  setContent(newContent: string) {
    this.content = newContent;
  }

  ngOnInit(): void {}
}
