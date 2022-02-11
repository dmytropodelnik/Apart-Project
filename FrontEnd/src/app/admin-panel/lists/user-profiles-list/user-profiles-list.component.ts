import { Component, OnInit } from '@angular/core';
import { UserProfile } from 'src/app/models/UserData/useprofile.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-user-profiles-list',
  templateUrl: './user-profiles-list.component.html',
  styleUrls: ['./user-profiles-list.component.css']
})
export class UserProfilesListComponent implements OnInit {

  profiles: UserProfile[] | null = null;
  profile: string | null = null;
  checkedProfile: number | null = null;

  constructor() {}

  addProfile(): void {
    let profile = {
      name: this.profile,
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
        } else {
          alert('Adding error!');
        }
        this.profile = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editProfile(): void {
    let profile = {
      id: this.checkedProfile,
      name: this.profile,
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
        } else {
          alert('Editing error!');
        }
        this.profile = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteProfile(): void {
    let profile = {
      id: this.checkedProfile,
      name: this.profile,
    };

    fetch('https://localhost:44381/api/profiles/deleteprofile', {
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
        } else {
          alert('Editing error!');
        }
        this.profile = '';
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

  setProfile(id: number | null, profile: string): void {
    this.checkedProfile = id;
    this.profile = profile;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getProfiles();
  }

}
