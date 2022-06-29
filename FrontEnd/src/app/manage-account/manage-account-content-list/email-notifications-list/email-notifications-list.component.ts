import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from 'src/app/services/main-data.service';

@Component({
  selector: 'app-email-notifications-list',
  templateUrl: './email-notifications-list.component.html',
  styleUrls: ['./email-notifications-list.component.css'],
})
export class EmailNotificationsListComponent implements OnInit {
  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {}
}
