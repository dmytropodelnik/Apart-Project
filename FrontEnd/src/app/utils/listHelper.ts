//eslint-disable-next-line import/no-anonymous-default-export
export default {
  disableButtons(): void {
    document.getElementById('editButton')?.setAttribute('disabled', 'disabled');
    document.getElementById('deleteButton')?.setAttribute('disabled', 'disabled');
  }
}
