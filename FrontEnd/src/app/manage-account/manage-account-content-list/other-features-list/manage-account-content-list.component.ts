import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from 'src/app/services/main-data.service';

@Component({
  selector: 'app-manage-account-content-list',
  templateUrl: './manage-account-content-list.component.html',
  styleUrls: ['./manage-account-content-list.component.css'],
})
export class ManageAccountContentListComponent implements OnInit {
  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {}
}
