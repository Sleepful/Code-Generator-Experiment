/////////////////////////////////////////////////////////////

      case GeneralManagementEnum.{!FIELDS}:
        this.hasManagementAddEditPermission = this.userSessionService
          .hasPermission(ModulesEnum.{!MODULE_ENUM}, AccessLevelEnum.AddEdit);
        this.hasManagementDeletePermission = this.userSessionService
          .hasPermission(ModulesEnum.{!MODULE_ENUM}, AccessLevelEnum.Delete);

        this.statusTitle = this.translatePipe.transform('common-management', 'systemStatus').value;
        this.createTitle = this.translatePipe.transform('common-management', 'new{!FIELD}').value;
        this.editTitle = this.translatePipe.transform('common-management', 'edit{!FIELD}').value;
        this.detailTitle = this.translatePipe.transform('common-management', '{!FIELD_CC}Detail').value;
        this.confirmDeleteTitle = this.translatePipe.transform('common-management', '{!FIELD_CC}ConfirmDeleteMessage').value;

        break;

/////////////////////////////////////////////////////////////

        case GeneralManagementEnum.{!FIELDS}:

          this.subscriptions.add(
          this.listEntitiesEditService.get{!FIELD}ById(this.entityId).subscribe((data) => {
            this.setEntityData(data);
          }));

          break;


/////////////////////////////////////////////////////////////

      case GeneralManagementEnum.{!FIELDS}:

        operation = this.listEntitiesEditService.add{!FIELD}(this.entityModel);
        this.handleDialogEntityOperation(operation, 'common-management', '{!FIELD_CC}Created');

        break;

/////////////////////////////////////////////////////////////

      case GeneralManagementEnum.{!FIELDS}:

        operation = this.listEntitiesEditService.update{!FIELD}(this.entityModel);
        this.handleEntityOperation(operation, 'common-management', '{!FIELD_CC}Updated');

        break;

/////////////////////////////////////////////////////////////


          case GeneralManagementEnum.{!FIELDS}:

            operation = this.listEntitiesEditService.delete{!FIELD}(this.entityId);
            this.handleEntityOperation(operation, 'common-management', '{!FIELD_CC}Deleted');

            break;