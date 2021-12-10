from PIL import Image
import random

imageSize = 1000
patternSize = 100
steps = int(imageSize/patternSize)

pattern = [[random.randint(0,1) for j in range(patternSize)] for i in range(patternSize)]

imageData = [[0 for j in range(imageSize)] for i in range(imageSize)]
for i in range(imageSize):
    for j in range(imageSize):
        imageData[i][j] = pattern[i//steps][j//steps]

dataStream = []
for i in range(imageSize):
    for j in range(imageSize):
        dataStream += [imageData[i][j]]

img = Image.new("1", (imageSize, imageSize))
img.putdata(dataStream)
img.save("noise.png")