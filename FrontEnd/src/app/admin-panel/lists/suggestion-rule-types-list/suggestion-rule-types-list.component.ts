import { Component, OnInit } from '@angular/core';
import { SuggestionRuleType } from 'src/app/models/Suggestions/suggestionruletype.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';

@Component({
  selector: 'app-suggestion-rule-types-list',
  templateUrl: './suggestion-rule-types-list.component.html',
  styleUrls: ['./suggestion-rule-types-list.component.css']
})
export class SuggestionRuleTypesListComponent implements OnInit {

  ruleTypes: SuggestionRuleType[] | null = null;
  type: SuggestionRuleType | null = null;
  searchType: string = '';
  checkedType: number | null = null;
  imageHelper: any = ImageHelper;

  constructor() {}

  search(): void {
    fetch('https://localhost:44381/api/suggestionruletypes/search?type=' + this.searchType, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.ruleTypes = data.ruleTypes;
        } else {
          alert('Search error!');
        }
        this.searchType = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addType(): void {
    let type = {
      name: this.type,
    };

    fetch('https://localhost:44381/api/suggestionruletypes/addtype', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
        } else {
          alert('Adding error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editType(): void {
    let type = {
      id: this.checkedType,
      name: this.type,
    };

    fetch('https://localhost:44381/api/suggestionruletypes/edittype', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteType(): void {
    let type = {
      id: this.checkedType,
      name: this.type,
    };

    fetch('https://localhost:44381/api/suggestionruletypes/deletetype', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getTypes(): void {
    fetch('https://localhost:44381/api/suggestionruletypes/gettypes', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.ruleTypes = data.types;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setType(type: SuggestionRuleType): void {
    this.checkedType = type.id;
    this.type = type;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getTypes();
  }

}
