import { Component, OnInit } from '@angular/core';
import { Address } from 'src/app/models/Location/address.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-addresses-list',
  templateUrl: './addresses-list.component.html',
  styleUrls: ['./addresses-list.component.css']
})
export class AddressesListComponent implements OnInit {

  addresses: Address[] | null = null;
  address: Address | null = null;
  searchAddress: string = '';
  checkedAddress: number | null = null;

  constructor() {}

  search(): void {
    fetch('https://localhost:44381/api/addresses/search?address=' + this.searchAddress, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.addresses = data.addresses;
        } else {
          alert('Search error!');
        }
        this.searchAddress = '';
      })
      .catch((ex) => {
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(address),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAddresses();
        } else {
          alert('Adding error!');
        }
        this.address = null;
      })
      .catch((ex) => {
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(address),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAddresses();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.address = null;
      })
      .catch((ex) => {
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(address),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAddresses();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.address = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getAddresses(): void {
    fetch('https://localhost:44381/api/addresses/getaddresses', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.addresses = data.addresses;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
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
