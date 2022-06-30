import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Address } from 'src/app/models/Location/address.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-addresses-list',
  templateUrl: './addresses-list.component.html',
  styleUrls: ['./addresses-list.component.css'],
})
export class AddressesListComponent implements OnInit {
  addresses: Address[] | null = null;
  address: Address | null = null;
  searchAddress: string = '';
  checkedAddress: number | null = null;

  page: number = 1;
  pageSize: number = 10;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch(
      'https://localhost:44381/api/addresses/search?address=' +
        this.searchAddress,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.addresses = data.addresses;
        } else {
          this.showAlert('Search error!');
        }
        this.searchAddress = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  addAddress(): void {
    let address = {
      name: this.address,
    };

    fetch('https://localhost:44381/api/addresses/addaddress', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(address),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAddresses();
        } else {
          this.showAlert('Adding error!');
        }
        this.address = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  editAddress(): void {
    let address = {
      id: this.checkedAddress,
      name: this.address,
    };

    fetch('https://localhost:44381/api/addresses/editaddress', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(address),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAddresses();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.address = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deleteAddress(): void {
    let address = {
      id: this.checkedAddress,
      name: this.address,
    };

    fetch('https://localhost:44381/api/addresses/deleteaddress', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(address),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAddresses();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.address = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getAddresses(): void {
    fetch(
      `https://localhost:44381/api/addresses/getaddresses?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.addresses = data.addresses;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(addresses: Address[]): void {
    for (let item of addresses) {
      this.addresses?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/addresses/getaddresses?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.addresses);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  setAddress(address: Address): void {
    this.checkedAddress = address.id;
    this.address = address;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getAddresses();
  }
}
