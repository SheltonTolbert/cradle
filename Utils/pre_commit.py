from pathlib import Path
import os
import fileinput

# Recursively iterate through file system pulling out .cs files
def getFiles(parent, memo = []):
    path = Path(parent)

    for path in path.iterdir():
        if path.is_dir(): 
            memo = getFiles(path, memo)
        if str(path).endswith('.cs'):
            memo.append(path)

    return memo

# Parse each file for doc strings
# A doc string should be formated as a multiline comment prepended with the '@docs' string

def generateDocs(paths):
    
    output = ''

    for path in paths:
        file = open(path, 'r')
        lines = file.readlines()
        docString = ''
        readingDocString = False
        filename = (str(path).split('/')[-1]).split('.')[0] + '_doc.md'

        for line in lines:
            if '@docs' in line: 
                readingDocString = True
            if readingDocString: 
                docString = docString + '\n' + line
            if '*/' in line:
                docString = docString + '\n'
                readingDocString = False
        output += (docString.replace('*/','').replace('/*','').replace('@docs', ''))
    return output


def writeToFile(docs,destinationPath):
    try: 
        docFile  = open(destinationPath, 'w')
        docFile.write(docs)
        docFile.close()
    except: 
        print(f'ERROR: invalid path, check {destinationPath} exists')

def main():
    print('Generating Docs...')
    output = generateDocs(getFiles('../Cradle/Assets/Scripts/'))
    writeToFile(output, '../Cradle/Assets/Docs/docs.md')
    
if __name__ == '__main__':
    main()