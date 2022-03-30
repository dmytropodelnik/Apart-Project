import { Component, OnInit } from '@angular/core';
import { UserProfile } from 'src/app/models/UserData/useprofile.item';
import { isThisTypeNode } from 'typescript';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';

@Component({
  selector: 'app-user-profiles-list',
  templateUrl: './user-profiles-list.component.html',
  styleUrls: ['./user-profiles-list.component.css']
})
export class UserProfilesListComponent implements OnInit {

  profiles: UserProfile[] | null = null;
  birthDate: string | null = null;
  searchProfile: string = '';
  checkedProfile: number | null = null;
  imageHelper: any = ImageHelper;

  page: number = 1;
  pageSize: number = 10;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://localhost:44381/api/userprofiles/search?profile=' + this.searchProfile, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.profiles = data.profiles;
        } else {
          alert('Search error!');
        }
        this.searchProfile = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addProfile(): void {
    let profile = {
      birthDate: this.birthDate,
    };

    fetch('https://localhost:44381/api/userprofiles/addprofile', {
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

    fetch('https://localhost:44381/api/userprofiles/editprofile', {
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

    fetch('https://localhost:44381/api/userprofiles/deleteprofile', {
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
    fetch(`https://localhost:44381/api/userprofiles/getprofiles?page=${this.page}&pageSize=${this.pageSize}`, {
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

  collectElements(profiles: UserProfile[]): void {
    for (let item of profiles) {
      this.profiles?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://localhost:44381/api/userprofiles/getprofiles?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.profiles);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setProfile(profile: UserProfile): void {
    this.checkedProfile = profile.id;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getProfiles();
  }
}
