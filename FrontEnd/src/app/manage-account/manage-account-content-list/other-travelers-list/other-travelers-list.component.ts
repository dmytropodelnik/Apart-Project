import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from 'src/app/services/main-data.service';

@Component({
  selector: 'app-other-travelers-list',
  templateUrl: './other-travelers-list.component.html',
  styleUrls: ['./other-travelers-list.component.css'],
})
export class OtherTravelersListComponent implements OnInit {
  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {}
}
