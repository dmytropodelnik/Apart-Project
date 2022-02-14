import { Component, OnInit } from '@angular/core';
import { UserProfile } from 'src/app/models/UserData/useprofile.item';
import { isThisTypeNode } from 'typescript';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-user-profiles-list',
  templateUrl: './user-profiles-list.component.html',
  styleUrls: ['./user-profiles-list.component.css']
})
export class UserProfilesListComponent implements OnInit {

  profiles: UserProfile[] | null = null;
  birthDate: string | null = null;
  checkedProfile: number | null = null;

  constructor() {}

  addProfile(): void {
    let profile = {
      birthDate: this.birthDate,
    };

    fetch('https://localhost:44381/api/profiles/addprofile', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(profile),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getProfiles();
          this.disableButtons();
        } else {
          alert('Adding error!');
        }
        this.birthDate = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editProfile(): void {
    let profile = {
      id: this.checkedProfile,
      birthDate: this.birthDate,
    };

    fetch('https://localhost:44381/api/profiles/editprofile', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(profile),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getProfiles();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.birthDate = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  disableButtons(): void {
    document.getElementById('editButton')?.setAttribute('disabled', 'disabled');
    document.getElementById('deleteButton')?.setAttribute('disabled', 'disabled');
  }

  deleteProfile(): void {
    let profile = {
      id: this.checkedProfile,
      birthDate: this.birthDate,
    };

    fetch('https://localhost:44381/api/profiles/deleteprofile', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(profile),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getProfiles();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.birthDate = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getProfiles(): void {
    fetch('https://localhost:44381/api/profiles/getprofiles', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.profiles = data.profiles;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setProfile(id: number | null, birthDate: string | null): void {
    this.checkedProfile = id;
    this.birthDate = birthDate;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getProfiles();
  }
}
