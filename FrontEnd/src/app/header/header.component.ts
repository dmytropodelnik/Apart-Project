import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isExist: string;
  public isCollapsed = false;

  @ViewChild('content') content !: TemplateRef<any>;
  @ViewChild('content1') content1 !: TemplateRef<any>;

  constructor(config: NgbModalConfig, private modalService: NgbModal) {
    this.isExist = '';
  }

  ngOnInit(): void {
  }

  open() {
    this.modalService.open(this.content, { size: 'lg', centered: true });
  }

  open1() {
    this.modalService.open(this.content1, { size: 'lg', centered: true });
  }

}
