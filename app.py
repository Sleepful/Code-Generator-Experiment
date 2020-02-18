import re
import datetime

#Begin automation
print('Hello, metaprogramming!')
area = 'PayrollSetup'
field = 'PaymentFrequency'
fields = 'PaymentFrequencies'
urls = 'payment-frequencies'
parentEnum = 'TaxesManagement'

def main():
  initialize()
  # fillEntity()
  # fillController()
  # fillService()
  # fillRetrieveByFilterSproc()
  # fillDbContext()
  # fillAutoMapping()
  # fillModuleMigration()
  # fillSprocMigration()
  
  print('\nremember to add migration for entity:')
  print('    * dotnet ef migrations add add-utb'+fields)
  #remember to add migration for sproc
  print('\nremember to add migration for sproc:')
  print('    * dotnet ef migrations add add-uspRetrieve'+fields+'ByFilter')
  #remmeber to add new enum modules if necessary
  print('\nremember to add new enum modules if necessary:')
  print('    > modify: ModuleEnum.cs')
  print('    * dotnet ef migrations add add-ModulePermissions'+fields)
  print('\nUpdate :D')
  print('    * dotnet ef database update')

# generate DatabaseContext stuff based on entity file?
def generateKeyNames():
  print('hehehe')

def replaceContent(data):
  for var, pattern in pairsToReplace:
    data = re.sub(re.escape(pattern), var, data)
  return data

def fillSprocMigration():
  fillTemplates(
    'SprocMigration.cs',
    'SprocMigration.cs',
    replaceContent)

def fillModuleMigration():
  fillTemplates(
    'ModuleMigration.cs',
    'ModuleMigration.cs',
    replaceContent)

def fillAutoMapping():
  fillTemplates(
    'AutoMapping.cs',
    'AutoMapping.cs',
    replaceContent)

def fillEntity():
  fillTemplates(
    'Entity.cs',
    field+'.cs',
    replaceContent)

def fillDbContext():
  fillTemplates(
    'DatabaseContext.cs',
    'DatabaseContext.cs',
    replaceContent)

def fillController():
  fillTemplates(
    'Controller.cs',
    field+'Controller.cs',
    replaceContent)

def fillService():
  fillTemplates(
    'Service.cs',
    field+'Service.cs',
    replaceContent)

def fillRetrieveByFilterSproc():
  fillTemplates(
    'uspRetrieveByFilter.sql',
    'uspRetrieve'+fields+'ByFilter.sql',
    replaceContent)

def fillTemplates(source, target, manipulation):
  data = '';
  with open(templatePath+source, 'r') as f:
    data = f.read()
  data = manipulation(data)
  with open(outputPath+target, 'w') as f:
    f.write(data)


def initialize():
  global templatePath
  global outputPath
  global pairsToReplace
  date = datetime.datetime.today().strftime('%d-%m-%Y')
  # service variables
  p_area = "{!AREA}"
  p_field = "{!FIELD}" #first with case
  p_fields = "{!FIELDS}" #plural, first with case
  p_field_cc = "{!FIELD_CC}" #camel case

  # sproc var
  p_date = "{!DATE}" #current date for SPROCS

  # controller variables
  p_urls = "{!URLS}" #for example 'withholding-statuses'

  #check if ModuleEnum.{!FIELDS} exists
  # if not: generate migration file?

  p_parentEnum = "{!OTHER_ENUM}" # routes accesible from other management places

  templatePath = 'Templates\GeneralCrud\\'
  outputPath = 'Output\\'

  field_cc = field[:1].lower() + field[1:]
  pairsToReplace = [(area, p_area), (field, p_field), (fields, p_fields),
              (urls, p_urls), (parentEnum, p_parentEnum), (date, p_date), 
              (field_cc, p_field_cc)]

main()

