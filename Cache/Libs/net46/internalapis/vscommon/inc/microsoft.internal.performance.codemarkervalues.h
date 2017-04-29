











































































namespace Microsoft {
    namespace Internal {
        namespace Performance {
            
            

#ifdef Codemarkers_IncludeAppEnum
                
                 enum CodeMarkerApp {
                    
                    UNDEFINEDPERF = 50,
                    
                    WORDPERF = 1,
                    
                    EXCELPERF = 2,
                    
                    ACCESSPERF = 3,
                    
                    PPTPERF = 4,
                    
                    OUTLOOKPERF = 5,
                    
                    ZENPERF = 6,
                    
                    SBTPERF = 7,
                    
                    GRAPHPERF = 8,
                    
                    MSOPERF = 9,
                    
                    PROJECTPERF = 12,
                    
                    WEBSERVERPERF = 18,
                    
                    IEPERF = 27,
                    
                    FRONTPAGEPERF = 30,
                    
                    PUBLISHERPERF = 31,
                    
                    PHOTODRAWPERF = 32,
                    
                    HELPPERF = 33,
                    
                    NSEPERF = 34,
                    
                    VISIOPERF = 35,
                    DESIGNERPERF = 36,
                    XDOCSPERF = 37,
                    
                    VSTORTPERF = 42,
                    
                    VSDEVENVPERF = 51,
                    
                    VSMSDNPERF = 52,
                    
                    VSVSAENVPERF = 53,
                    
                    VSDBGPERF = 54,
                    
                    VSMODPERF = 55,
                    
                    VSISOSHELLPERF = 56,
                    
                    VS----PPERF = 57,
                    
                    VWDEXPRESSPERF = 58,
                    
                    VBEXPRESSPERF = 59,
                    
                    VCEXPRESSPERF = 60,
                    
                    VC----PRESSPERF = 61,
                    
                    VJ----PRESSPERF = 62,
                    
                    CLICKONCEPERF = 63,
                    
                    DEVEMULATORPERF = 65,
                    
                    RASCALPERF = 66,
                    
                    MBFDEPLOYMENT = 70,
                    
                    VSTAIDEPERF = 71,
                    
                    VSDDEMPERF = 72,
                    
                    WCFTOOLSPERF = 73,
                    
                    MSBUILDPERF = 74,
                    
                    VPDEXPRESSPERF = 75,
                    
                    VSWINEXPRESSPERF = 76,
                    
                    SCRIPTEDPLUGINPERF = 77,
                    
                    WDEXPRESSPERF = 78,
                    
                    DIAGNOSTICSHUBPERF = 79,
                    
                    VSWEBHANDLERPERF = 80,
                    
                    VSWEBLAUNCHERPERF = 81,
                }; 
#endif 

            
            
             enum CodeMarkerEvent {




#ifdef Codemarkers_IncludeClickOnceMarkers

                    perfPersisterWriteStart = 8080,
                    perfPersisterWriteEnd = 8081,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeOfficeMarkers)
                    
                    perfBeginSession = 1,
                    perfSeatBeltEndSession = 2,
                    
                    perfCPUFrequency = 3,
                    perfInitOverheadStart = 4,
                    perfInitOverheadStop = 5,
                    perfUnInitOverheadStart = 6,
                    perfUnInitOverheadStop = 7,
                    perfCalibrate1 = 8,     
                    
                    
                    perfCalibrate2 = 9,
                    perfREBOOT = 10,

                    
                    perfBootStart = 500,
                    perfBootStop = 501,
                    perfIdle = 502,
                    perfOpenStart = 503,
                    perfOpenEnd = 504,
                    perfPrintStart = 505,
                    perfPrintEnd = 506,
                    perfSaveStart = 507,
                    perfSaveEnd = 508,
                    perfNextSlide = 509,
                    perfSlideShowBegin = 510,
                    perfNewFileBegin = 511,     
                    perfDialogBegin = 512,
                    perfPrintReturnControl = 513,
                    perfSlideShowBackSlide = 514,
                    perfOLEInsertBegin = 515,
                    perfSlideViewScrollBegin = 516,
                    perfNewMessageBegin = 517,
                    perfNewMessageEnd = 518,
                    perfNewAddrBegin = 519,
                    perfNewAddrEnd = 520,
                    perfNewNoteBegin = 521,
                    perfNewNoteEnd = 522,
                    perfNewTaskBegin = 523,
                    perfNewTaskEnd = 524,
                    perfNewApptBegin = 525,
                    perfNewApptEnd = 526,
                    perfNewDistListBegin = 527,
                    perfNewDistListEnd = 528,
                    perfOnDeliverMailStart = 529,
                    perfOnDeliverMailStop = 530,
                    perfSelectTopItemBegin = 531,
                    perfSelectTopItemEnd = 532,
                    perfReplyToItemBegin = 533,
                    perfReplyToItemEnd = 534,
                    perfOnMailSendBegin = 535,
                    perfOnMailSendEnd = 536,
                    perfDeleteItemBegin = 537,
                    perfDeleteItemEnd = 538,
                    perfOpenAttachBegin = 539,
                    perfOpenAttachEnd = 540,
                    perfDeleteItemInspectorBegin = 541,
                    perfDeleteItemInspectorEnd = 542,
                    perfCheckNameBegin = 543,
                    perfCheckNameEnd = 544,
                    perfEndBootAtInit = 545,
                    perfChangeFolderBegin = 546,
                    perfChangeFolderEnd = 547,
                    perfExitBegin = 548,
                    perfExitEnd = 549,
                    perfMoveItemtoFolderBegin = 550,
                    perfMoveItemtoFolderEnd = 551,
                    perfEmptyDelItemsBegin = 552,
                    perfEmptyDelItemsEnd = 553,
                    perfAcceptMRBegin = 554,
                    perfAcceptMREnd = 555,
                    perfGoToDateBegin = 556,
                    perfGoToDateEnd = 557,
                    perfFindOrgBegin = 558,
                    perfFindOrgEnd = 559,
                    perfOutlookTodayBegin = 560,
                    perfOutlookTodayEnd = 561,
                    perfExcelRecalcBegin = 562,
                    perfExcelRecalcEnd = 563,
                    perfCopyBegin = 564,
                    perfCopyEnd = 565,
                    perfPasteBegin = 566,
                    perfPasteEnd = 567,
                    perfExcelPivotTableWizardBegin = 568,
                    perfExcelPivotTableWizardEnd = 569,
                    perfWordRepagStart = 570,
                    perfWordRepagStop = 571,
                    perfWordScrollStart = 572,
                    perfWordScrollStop = 573,
                    perfCutBegin = 574,
                    perfCutEnd = 575,
                    perfInsertBegin = 576,
                    perfInsertEnd = 577,
                    perfExcelRunMacroBegin = 578,
                    perfExcelRunMacroEnd = 579,
                    perfExcelClearAllBegin = 580,
                    perfExcelClearAllEnd = 581,
                    perfGroupObjBegin = 582,
                    perfGroupObjEnd = 583,
                    perfUngroupObjBegin = 584,
                    perfUngroupObjEnd = 585,
                    perfExcelScrollPaneBegin = 586,
                    perfExcelScrollPaneEnd = 587,
                    perfExcelColBestFitBegin = 588,
                    perfExcelColBestFitEnd = 589,
                    perfExcelDrawPaneBegin = 590,
                    perfExcelDrawPaneEnd = 591,
                    perfExcelDrawingCommandBegin = 592,
                    perfExcelDrawingCommandEnd = 593,
                    perfShowVBEBegin = 594,
                    perfShowVBEEnd = 595,
                    perfExcelDrawChartBegin = 596,
                    perfExcelDrawChartEnd = 597,
                    perfNewDatabaseBegin = 598,
                    perfNewDatabaseEnd = 599,
                    perfOpenDatabaseBegin = 600,
                    perfOpenDatabaseEnd = 601,
                    perfOpenObjectBegin = 602,
                    perfOpenObjectEnd = 603,
                    perfWizardBegin = 604,
                    perfWizardEnd = 605,
                    perfWizardReady = 606,
                    perfBuilderBegin = 607,
                    perfBuilderEnd = 608,
                    perfBuilderReady = 609,
                    perfFrontPageNewWebBegin = 610,
                    perfFrontPageNewWebEnd = 611,
                    perfFrontPageOpenWebBegin = 612,
                    perfFrontPageOpenWebEnd = 613,
                    perfFrontPageCloseWebBegin = 614,
                    perfFrontPageCloseWebEnd = 615,
                    perfFrontPageReportsViewBegin = 616,
                    perfFrontPageReportsViewEnd = 617,
                    perfFrontPageReportsSummaryBegin = 618,
                    perfFrontPageReportsSummaryEnd = 619,
                    perfOutlookForwardItemBegin = 620,
                    perfOutlookForwardItemEnd = 621,
                    perfOutlookCloseItemBegin = 622,
                    perfOutlookCloseItemEnd = 623,
                    perfOwcCreateObjectBegin = 624,
                    perfOwcCreateObjectEnd = 625,
                    perfOwcFreezeEventsBegin = 626,
                    perfOwcFreezeEventsEnd = 627,
                    perfAtlPersistPropBagLoadBegin = 628,
                    perfAtlPersistPropBagLoadEnd = 629,
                    perfOwcLoadObjectBegin = 630,
                    perfOwcLoadObjectEnd = 631,
                    perfAtlQuickActivateBegin = 632,
                    perfAtlQuickActivateEnd = 633,
                    perfOwcGenericEventInvokeBegin = 634,
                    perfOwcGenericEventInvokeEnd = 635,
                    perfAtlPersistPropBagSaveBegin = 636,
                    perfAtlPersistPropBagSaveEnd = 637,
                    perfAtlViewObjectDrawBegin = 638,
                    perfAtlViewObjectDrawEnd = 639,
                    perfOwcFinalReleaseBegin = 640,
                    perfOwcFinalReleaseEnd = 641,
                    perfOutlookReplyForwardOnItemBegin = 642,
                    perfOutlookReplyForwardOnItemEnd = 643,
                    perfExcelMoveCopySheetBegin = 644,
                    perfExcelMoveCopySheetEnd = 645,
                    perfWordNewStart = 646,
                    perfWordNewEnd = 647,
                    perfWordCountStart = 648,
                    perfWordCountEnd = 649,
                    perfWordOutlineViewStart = 650,
                    perfWordOutlineViewEnd = 651,
                    perfWordPageViewStart = 652,
                    perfWordPageViewEnd = 653,
                    perfWordNormalViewStart = 654,
                    perfWordNormalViewEnd = 655,
                    perfWordWebViewStart = 656,
                    perfWordWebViewEnd = 657,
                    perfWordFindNextStart = 658,
                    perfWordFindNextEnd = 659,
                    perfWordFindAllStart = 660,
                    perfWordFindAllEnd = 661,
                    perfWordReplaceAllStart = 662,
                    perfWordReplaceAllEnd = 663,
                    perfWordAutoFormatStart = 664,
                    perfWordAutoFormatEnd = 665,
                    perfWordInsPictureStart = 666,
                    perfWordInsPictureEnd = 667,
                    perfWordInsBookmarkStart = 668,
                    perfWordInsBookmarkEnd = 669,
                    perfWordInsSymbolStart = 670,
                    perfWordInsSymbolEnd = 671,
                    perfWordInsObjectStart = 672,
                    perfWordInsObjectEnd = 673,
                    perfWordInsTocStart = 674,
                    perfWordInsTocEnd = 675,
                    perfWordSpellStart = 676,
                    perfWordSpellEnd = 677,
                    perfWordGrammarStart = 678,
                    perfWordGrammarEnd = 679,
                    perfWordInsCommentStart = 680,
                    perfWordInsCommentEnd = 681,
                    perfIOLDocUploadStart = 682,
                    perfIOLDocUploadEnd = 683,
                    perfIOLDocDownloadStart = 684,
                    perfIOLDocDownloadEnd = 685,
                    perfHlinkDownloadStart = 686,
                    perfHlinkDownloadEnd = 687,
                    perfPostNavigateStart = 688,
                    perfPostNavigateEnd = 689,
                    perfNavigateStart = 690,
                    perfNavigateEnd = 691,
                    perfNSEDeleteStart = 692,
                    perfNSEDeleteEnd = 693,
                    perfNSEDragDropStart = 694,
                    perfNSEDragDropEnd = 695,
                    perfNSEEnumStart = 696,
                    perfNSEEnumEnd = 697,
                    perfOutlookSaveCloseStart = 698,
                    perfOutlookSaveCloseEnd = 699,
                    perfExcelBkgndErrChkStart = 700,
                    perfExcelBkgndErrChkEnd = 701,
                    perfOutlookSyncOSTStart = 702,
                    perfOutlookSyncOSTEnd = 703,
                    perfAccessCompileBegin = 704,
                    perfAccessCompileEnd = 705,
                    perfAccessSaveProjectBegin = 706,
                    perfAccessSaveProjectEnd = 707,
                    perfPhdSolidColorFillStart = 708,
                    perfPhdFadeStart = 709,
                    perfPhdBlurSharpStart = 710,
                    perfPhdPhotoCorrectionStart = 711,
                    perfPhdDesignerEffectStart = 712,
                    perfPhdDrawAutoShapeStart = 713,
                    perfPhdPhotoArtisticBrushStart = 714,
                    perfPhdDesignerEdgeStart = 715,
                    perfPhdColorCorrectStart = 716,
                    perfPhdMoveStart = 717,
                    perfPhdResizeStart = 718,
                    perfPhdRotateStart = 719,
                    perfPhd3DStart = 720,
                    perfPhdInsertTextStart = 721,
                    perfPhdUpdateTextStart = 722,
                    perfPhdTemplatesStart = 723,
                    perfPhdDocSwitchStart = 724,
                    perfPhdWorkpaneStart = 725,
                    perfPhdZoomStart = 726,
                    perfPhdCropStart = 727,
                    perfPhdCutOutStart = 728,
                    perfPPTApplyTemplateStart = 729,
                    perfPPTChangeView = 730,
                    perfPPTAddMaster = 731,
                    perfPPTDeleteMaster = 732,
                    perfFrontPageWebProvisionBegin = 733,
                    perfFrontPageWebProvisionEnd = 734,
                    perfFrontPageEnsureFolderBegin = 735,
                    perfFrontPageEnsureFolderEnd = 736,
                    perfFrontPageDownloadFileBegin = 737,
                    perfFrontPageDownloadFileEnd = 738,
                    perfFrontPageBrowserOpBegin = 739,
                    perfFrontPageBrowserOpEnd = 740,
                    perfFrontPageUploadFileBegin = 741,
                    perfFrontPageUploadFileEnd = 742,
                    perfOfficeHlinkDialogBootBegin = 743,
                    perfOfficeHlinkDialogBootEnd = 744,
                    perfOfficeHlinkDialogBegin = 745,
                    perfOfficeHlinkDialogReady = 746,
                    perfOwcPageInteractive = 747,
                    perfOutlookContactQuickFindBegin = 748,
                    perfOutlookContactQuickFindEnd = 749,
                    perfOfficeFileSaveDlgBegin = 750,
                    perfSCPCodeVerBegin = 751, 
                    perfSCPCodeVerEnd = 752,   
                    perfFrontPageBlockingRpcBegin = 753,
                    perfFrontPageBlockingRpcEnd = 754,
                    perfFrontPageListUrlsBegin = 755,
                    perfFrontPageListUrlsEnd = 756,
                    perfFrontPageEnsureFullListBegin = 757,
                    perfFrontPageEnsureFullListEnd = 758,
                    perfFrontPageFolderViewBegin = 759,
                    perfFrontPageFolderViewEnd = 760,
                    perfFrontPageStructureViewBegin = 761,
                    perfFrontPageStructureViewEnd = 762,
                    perfFrontPagePageViewBegin = 763,
                    perfFrontPagePageViewEnd = 764,
                    perfFrontPageTodoViewBegin = 765,
                    perfFrontPageTodoViewEnd = 766,
                    perfFrontPageUsageViewBegin = 767,
                    perfFrontPageUsageViewEnd = 768,
                    perfFrontPageHyperLinkViewBegin = 769,
                    perfFrontPageHyperLinkViewEnd = 770,
                    perfFrontPageSaveStructureBegin = 771,
                    perfFrontPageSaveStructureEnd = 772,
                    perfFrontPagePutDocMetaBegin = 773,
                    perfFrontPagePutDocMetaEnd = 774,
                    perfFrontPageRecalcBegin = 775,
                    perfFrontPageRecalcEnd = 776,
                    perfFrontPageFolderExpandBegin = 777,
                    perfFrontPageFolderExpandEnd = 778,
                    perfFrontPageFolderContractBegin = 779,
                    perfFrontPageFolderContractEnd = 780,
                    perfFrontPageCrossWebFindBegin = 781,
                    perfFrontPageCrossWebFindEnd = 782,
                    perfFrontPageNewPageBegin = 783,
                    perfFrontPageNewPageEnd = 784,
                    perfFrontPageSharedBorderBegin = 785,
                    perfFrontPageSharedBorderEnd = 786,
                    perfFrontPageThemeBegin = 787,
                    perfFrontPageThemeEnd = 788,
                    perfFrontPageStructureDeletePageBegin = 789,
                    perfFrontPageStructureDeletePageEnd = 790,
                    perfOLViewAllProposeBegin = 791,
                    perfOLViewAllProposeEnd = 792,
                    perfOfficeArtZoomBegin = 793,
                    perfOfficeArtZoomEnd = 794,
                    PerfOfficeArtScrollBegin = 795,
                    PerfOfficeArtScrollEnd = 796,
                    PerfOfficeArtPasteBegin = 799,
                    PerfOfficeArtPasteEnd = 800,
                    PerfOfficeArtRotateSelectionBegin = 801,
                    PerfOfficeArtRotateSelectionEnd = 802,
                    PerfOfficeArtEditSelectionBegin = 803,
                    PerfOfficeArtEditSelectionEnd = 804,
                    PerfOfficeArtNudgeBegin = 805,
                    PerfOfficeArtNudgeEnd = 806,
                    PerfOfficeArtResizeBegin = 807,
                    PerfOfficeArtResizeEnd = 808,
                    PerfOLProposeNTBegin = 809,
                    PerfOLProposeNTEnd = 810,
                    perfFrontPageEditBegin = 811,
                    perfFrontPageEditEnd = 812,
                    perfFrontPageScrollBegin = 813,
                    perfFrontPageScrollEnd = 814,
                    perfFrontPageTimerBegin = 815,
                    perfFrontPageTimerEnd = 816,
                    perfFrontPageRenameBegin = 817,
                    perfFrontPageRenameEnd = 818,
                    perfFrontPagePublishBegin = 819,
                    perfFrontPagePublishEnd = 820,
                    perfFrontPageResizeBegin = 821,
                    perfFrontPageResizeEnd = 822,
                    perfOutlookSyncGroupBegin = 823,
                    perfOutlookSyncGroupEnd = 824,
                    perfOutlookSyncSubmitBegin = 825,
                    perfOutlookSyncSubmitEnd = 826,
                    perfOwcPostPerfInit = 827,
                    perfFrontPageClosePageBegin = 828,
                    perfFrontPageClosePageEnd = 829,
                    perfDatapageOpenStart = 830,
                    perfVisioZoomStart = 831,
                    perfVisioZoomEnd = 832,
                    perfVisioGroupStart = 833,
                    perfVisioGroupEnd = 834,
                    perfVisioPrintPreviewStart = 835,
                    perfVisioPrintPreviewEnd = 836,
                    perfVisioUndo = 837,
                    perfVisioRedo = 838,
                    perfVisioSelectAllBegin = 839,
                    perfVisioSelectAllEnd = 840,
                    perfVisioBatchLayoutBegin = 841,
                    perfVisioBatchLayoutEnd = 842,
                    perfVisioAddonStart = 843,
                    perfVisioAddonEnd = 844,
                    perfVisioVDXParseINodeStart = 845,
                    perfVisioVDXParseINodeEnd = 846,
                    perfVisioVDXParseDOMStart = 847,
                    perfVisioVDXParseDOMEnd = 848,
                    perfVisioUnionStart = 849,
                    perfVisioUnionEnd = 850,
                    perfVisioFragmentStart = 851,
                    perfVisioFragmentEnd = 852,
                    perfVisioCombineStart = 853,
                    perfVisioCombineEnd = 854,
                    perfVisioSetTextANSIStart = 855,
                    perfVisioSetTextANSIEnd = 856,
                    perfVisioInitInsertControlDlgStart = 857,
                    perfVisioInitInsertControlDlgEnd = 858,
                    perfVisioDropOnPageStart = 859,
                    perfVisioDropOnPageEnd = 860,
                    perfVisioRefreshViewStart = 861,
                    perfVisioRefreshViewEnd = 862,
                    perfVisioMoveObject = 863,
                    perfVisioMoveObjectEnd = 864,
                    perfVisioRefreshROMStart = 865,
                    perfVisioRefreshROMEnd = 866,
                    perfIERenderComplete = 867,
                    perfIEDone = 868,
                    perfOutlookViewChangedStart = 869,
                    perfOutlookViewChangedEnd = 870,
                    perfDesignerNewElementStart = 871,
                    perfDesignerNewElementStop = 872,
                    perfDesignerOpenFormStart = 873,
                    perfDesignerOpenFormStop = 874,
                    perfDesignerCreateFieldStart = 875,
                    perfDesignerCreateFieldStop = 876,
                    perfDesignerOpenEditorStart = 877,
                    perfDesignerOpenEditorStop = 878,
                    perfDesignerNewAppStart = 879,
                    perfDesignerNewAppStop = 880,
                    perfOutlookSearchFolderSearchStart = 881,
                    perfOutlookSearchFolderSearchEnd = 882,
                    perfWordSmartTagBkgCheckStart = 883,
                    perfWordSmartTagBkgCheckEnd = 884,
                    perfWordSmartTagFrgCheckStart = 885,
                    perfWordSmartTagFrgCheckEnd = 886,
                    perfDesignerCreateProjectStart = 887,
                    perfDesignerCreateProjectEnd = 888,
                    perfDesignerOpenProjectStart = 889,
                    perfDesignerOpenProjectEnd = 890,
                    perfOutlookViewSortStart = 891,
                    perfOutlookViewSortEnd = 892,
                    perfOutlookViewScrollStart = 893,
                    perfOutlookViewScrollEnd = 894,
                    perfDesignerAddFieldStart = 895,
                    perfDesignerAddFieldStop = 896,
                    perfDesignerBootWithProjectStop = 897,
                    perfDesignerUpdateFieldStart = 898,
                    perfDesignerUpdateFieldStop = 899,
                    perfDesignerLoadFieldChooserStart = 900,
                    perfDesignerLoadFieldChooserStop = 901,
                    perfDesignerUpdateFormRegStart = 902,
                    perfDesignerUpdateFormRegStop = 903,
                    perfDesignerSyncProjectStart = 904,
                    perfDesignerSyncProjectStop = 905,
                    perfDesignerToggleOfflineStart = 906,
                    perfDesignerToggleOfflineStop = 907,
                    perfAccessSUINavBegin = 908,
                    perfAccessSUINavEnd = 909,
                    perfCloseObjectBegin = 910,
                    perfCloseObjectEnd = 911,
                    perfOwcPivotInsertFieldSetBegin = 912,
                    perfOwcPivotInsertFieldSetEnd = 913,
                    perfOutlookItemViewNextPrevBegin = 914,
                    perfOutlookItemViewNextPrevEnd = 915,
                    perfNewClientBegin = 916,
                    perfNewClientEnd = 917,
                    perfNewFileEnd = 918,
                    perfNewFrameBegin = 919,
                    perfNewFrameEnd = 920,
                    perfSubmitFormBegin = 921,
                    perfSubmitFormEnd = 922,
                    perfBLgcScriptLoadBegin = 923,
                    perfBLgcScriptLoadEnd = 924,
                    perfBLgcScriptRunBegin = 925,
                    perfBLgcScriptRunEnd = 926,
                    PerfBLgcNodeValidationBegin = 927,
                    PerfBLgcNodeValidationEnd = 928,
                    perfGITreeGenBegin = 929,
                    perfGITreeGenEnd = 930,
                    perfSolutionLoadBegin = 931,
                    perfSolutionLoadEnd = 932,
                    perfXMLUndoBegin = 933,
                    perfXMLUndoEnd = 934,
                    perfXMLRedoBegin = 935,
                    perfXMLRedoEnd = 936,
                    perfIncrementalUpdateBegin = 937,
                    perfIncrementalUpdateEnd = 938,
                    perfMSOXEVIconBegin = 939,
                    perfMSOXEVIconEnd = 940,
                    perfMSOXEVLaunchBegin = 941,
                    perfMSOXEVLaunchEnd = 942,
                    perfViewChangeBegin = 943,
                    perfViewChangeEnd = 944,
                    perfLoadGIIntoViewBegin = 945,
                    perfLoadGIIntoViewEnd = 946,
                    perfXSLReapplyHTMLUpdateBegin = 947,
                    perfXSLReapplyHTMLUpdateEnd = 948,
                    perfCalculateTokenCurrentHTMLBegin = 949,
                    perfCalculateTokenCurrentHTMLEnd = 950,
                    perfCalculateTokenNewHTMLBegin = 951,
                    perfCalculateTokenNewHTMLEnd = 952,
                    perfCalculateHTMLDifferenceBegin = 953,
                    perfCalculateHTMLDifferenceEnd = 954,
                    perfChangeDifferenceScopeBegin = 955,
                    perfChangeDifferenceScopeEnd = 956,
                    perfGenerateDeltaChangeLogsBegin = 957,
                    perfGenerateDeltaChangeLogsEnd = 958,
                    perfCanvasDecodeBegin = 959,
                    perfCanvasDecodeEnd = 960,
                    perfCanvasExecBegin = 961,
                    perfCanvasExecEnd = 962,
                    perfComponentInsertBegin = 963,
                    perfComponentInsertEnd = 964,
                    perfSolutionComponentLoadBegin = 965,
                    perfSolutionComponentLoadEnd = 966,
                    perfSolutionComponentUnloadBegin = 967,
                    perfSolutionComponentUnloadEnd = 968,
                    perfSolutionComponentPaneLaunchBegin = 969,
                    perfSolutionComponentPaneLaunchEnd = 970,
                    perfXMLToXSDBegin = 971,
                    perfXMLToXSDEnd = 972,
                    perfXMLToXSDBuildDataStructuresBegin = 973,
                    perfXMLToXSDBuildDataStructuresEnd = 974,
                    perfXMLToXSDGenerateXSDBegin = 975,
                    perfXMLToXSDGenerateXSDEnd = 976,
                    perfApplyXSLReapplyBegin = 977,
                    perfApplyXSLReapplyEnd = 978,
                    perfOpen_NewCtrlLoadBegin = 979,
                    perfOpen_NewCtrlLoadEnd = 980,
                    perfOpen_DocSurfaceBegin = 981,
                    perfOpen_DocSurfaceEnd = 982,
                    perfOpen_ContextWorkBegin = 983,
                    perfOpen_ContextWorkEnd = 984,
                    perfSaveAsStart = 985,
                    perfSaveAsEnd = 986,
                    perfDataObjectLoadFromURLBegin = 987,
                    perfDataObjectLoadFromURLEnd = 988,
                    perfDataObjectSaveFromURLBegin = 989,
                    perfDataObjectSaveFromURLEnd = 990,
                    perfSubmitPreCheckBegin = 991,
                    perfSubmitPreCheckEnd = 992,
                    perfDataObjectCloseBegin = 993,
                    perfDataObjectCloseEnd = 994,
                    perfDataObjectSubmitBegin = 995,
                    perfDataObjectSubmitEnd = 996,
                    perfDataObjectLoadFromDocBegin = 997,
                    perfDataObjectLoadFromDocEnd = 998,
                    perfDataObjectSaveFromDocBegin = 999,
                    perfDataObjectSaveFromDocEnd = 1000,
                    perfXDocsBootBegin = 1001,
                    perfXDocsBootEnd = 1002,
                    perfBoldBegin = 1003,
                    perfBoldEnd = 1004,
                    perfItalicsBegin = 1005,
                    perfItalicsEnd = 1006,
                    perfHTMLUndoBegin = 1007,
                    perfHTMLUndoEnd = 1008,
                    perfHTMLRedoBegin = 1009,
                    perfHTMLRedoEnd = 1010,
                    perfInsertTableBegin = 1011,
                    perfInsertTableEnd = 1012,
                    perfInsertInternalTableBegin = 1013,
                    perfInsertInternalTableEnd = 1014,
                    perfInternalTableMoveBegin = 1015,
                    perfInternalTableMoveEnd = 1016,
                    perfInsertRowBegin = 1017,
                    perfInsertRowEnd = 1018,
                    perfInsertColBegin = 1019,
                    perfInsertColEnd = 1020,
                    perfInsertRowInternalBegin = 1021,
                    perfInsertRowInternalEnd = 1022,
                    perfInsertColInternalBegin = 1023,
                    perfInsertColInternalEnd = 1024,
                    perfInsertListBegin = 1025,
                    perfInsertListEnd = 1026,
                    perfRecalcBegin = 1027,
                    perfRecalcEnd = 1028,
                    perfInsertFFFBegin = 1029,
                    perfInsertFFFEnd = 1030,
                    perfCanvasActionBegin = 1031,
                    perfCanvasActionEnd = 1032,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSNativeMarkers)
                    
                    
                    perfVSShowMainWindow = 7000,
                    
                    perfVSStatusBarReady = 7001,
                    
                    perfVSLoadPropertyBrowserBegin = 7002,
                    
                    perfVSLoadPropertyBrowserEnd = 7003,
                    
                    perfVSInVStudioMain = 7004,
                    
                    perfVSStartPageCreated = 7005,
                    
                    perfVSDynamicHelpUpdate = 7006,
                    
                    perfVSLoadUIBegin = 7007,
                    
                    perfVSLoadUIEnd = 7008,
                    
                    perfVSBrowserDocumentComplete = 7009,
                    
                    perfVSInitThread = 7010,
                    
                    perfVSFindInFilesBegin = 7011,
                    
                    perfVSFindInFilesEnd = 7012,
                    
                    perfVSStatusBarBuildSucceeded = 7013,
                    
                    perfVSStatusBarRebuildSucceeded = 7014,

                    perfVSDebuggerEnterBreakState = 7015,
                    perfVSDebuggerSessionEnd = 7016,
                    perfVSDebuggerReceivesLoadCompleteEvent = 7017,
                    perfVSDebuggerReceivesEntryPointEvent = 7018,
                    perfVSDebuggerReceivesGoCommand = 7019,
                    perfVSDebuggerReceivesStartNoDebugCommand = 7020,
                    perfVSDebuggerReceivesStepIntoCommand = 7021,
                    perfVSDebuggerReceivesStepOverCommand = 7022,
                    perfVSDebuggerReceivesStepOutCommand = 7023,
                    perfVSDebuggerReceivesBreakCommand = 7024,
                    perfVSDebuggerReceivesStopCommand = 7025,
                    perfVSDebuggerReceivesRestartCommand = 7026,
                    perfVSDebuggerLaunchesAllTargets = 7027,
                    perfVSDebuggerSendsStartDebuggingRequest = 7028,
                    perfVSDebuggerLaunchesSingleTarget = 7029,
                    perfVSDebuggerAutoAttachComplete = 7030,
                    perfVSDebuggerAddBreakpoint = 7031,
                    perfVSDebuggerToggleBreakpoint = 7032,
                    perfVSDebuggerInsertBreakpoint = 7033,
                    

                    perfVSEditorNavigate = 7034,
                    perfVSEditorPasteBegin = 7035,
                    perfVSEditorPasteEnd = 7036,
                    perfVSEditorFileLoadBegin = 7037,
                    perfVSEditorFileLoadEnd = 7038,
                    perfVSEditorToolTipPaint = 7039,
                    perfVSEditorStatementCompletionPaint = 7040,
                    perfVSEditorCutBegin = 7041,
                    perfVSEditorCutEnd = 7042,
                    perfVSEditorWordWrapBegin = 7043,
                    perfVSEditorWordWrapEnd = 7044,
                    perfVSEditorStatementCompletionWordInsert = 7045,
                    perfVSEditorCommit = 7046,

                    perfVSProjectLoad = 7047,
                    perfVSFileOpen = 7048,
                    perfVSExternalToolComplete = 7049,

                    perfVSTaskListPopulated = 7050,
                    perfVSCVExpanded = 7051,
                    perfVSUIHierExpanded = 7052,
                    
                    perfVSClassViewPopulated = 7053,

                    perfVSEditGoToDeclaration = 7054,
                    perfVSEditGoToDefinition = 7055,
                    perfVSEditorDropDownDropped = 7056,

                    perfVSSolutionExplorerNavigation = 7057,
                    perfVSSolutionExplorerSolutionPopulated = 7058,
                    perfVSUIHierCollapsed = 7059,

                    
                    perfVSHelpFilterUpdated = 7060,
                    
                    perfVSHelpFilterCacheRecomputed = 7061,
                    
                    perfVSHelpFilterIndexUIUpdated = 7062,
                    
                    perfVSHelpFilterContentsUIUpdated = 7063,
                    
                    perfVSHelpFilterFTSResultsUIUpdated = 7064,
                    
                    perfVSHelpSearchCompleted = 7065,

                    
                    perfVSCloseSolution = 7066,
                    
                    perfVSSaveAll = 7067,

                    
                    perfVSDebuggingFinishedLoadingPackage = 7068,
                    
                    perfVSSolutionBeginDeploy = 7069,
                    
                    perfVSSolutionEndDeploy = 7070,
                    
                    perfVSStartPageLoadDownloadService = 7071,
                    
                    perfVSSplashScreenShowing = 7072,
                    
                    perfVSSplashScreenDestroyed = 7073,
                    
                    perfVSCloseAllPackagesBegin = 7074,
                    
                    perfVSCloseAllPackagesEnd = 7075,
                    
                    perfVSCoEEShutdownCOMBegin = 7076,
                    
                    perfVSCoEEShutdownCOMEnd = 7077,
                    
                    perfVSStopCLRBegin = 7078,
                    
                    perfVSStopCLREnd = 7079,

                    
                    
                    perfVSMacrosExplorerShowEnd = 7080,
                    
                    perfVSMacrosIDEShowEnd = 7081,
                    
                    perfVSMacrosMacroRunEnd = 7082,

                    
                    
                    perfVSStatusBarBuildFailed = 7090,
                    
                    perfVSStatusBarRebuildFailed = 7091,
                    
                    perfVSStatusBarBuildCanceled = 7092,
                    
                    perfVSStatusBarRebuildCanceled = 7093,

                    
                    perfVSToolboxSupportedCheckStart = 7094,
                    
                    perfVSToolboxSupportedCheckStop = 7095,
                    
                    perfVSToolboxResetDone = 7096,
                    
                    perfVSPrimeCLRNotScheduled = 7097,

                    
                    perfVSDebuggerScrollEnd = 7098,
                    perfVSDebuggerPaintEnd = 7099,

                    
                    perfVSHelpIndexLoadComplete = 7100,

                    
                    perfVSPrimeCLRBegin = 7101,
                    
                    perfVSPrimeCLREnd = 7102,
                    
                    perfVSFinishedBooting = 7103,
                    
                    perfVSNewProjectDlgComplete = 7104,

                    
                    
                    perfVSBrowserDocumentNavigateStart = 7105,

                    
                    
                    perfVSNewProjectDlgOpened = 7106,
                    
                    perfVSTemplateLoadBegin = 7107,
                    
                    perfVSTemplateLoadEnd = 7108,
                    
                    perfVSResourceManagerCacheMiss = 7109,

                    

                    
                    perfVSHelpF1CommandHandler = 7110,
                    
                    perfVSHelpF1ContextPacking = 7111,
                    
                    perfVSHelpF1RemoteF1Call = 7112,
                    
                    perfVSHelpF1ContextUnpacking = 7113,
                    
                    perfVSHelpF1LocalDataLookup = 7114,
                    
                    perfVSHelpF1LocalDataFound = 7115,
                    
                    perfVSHelpF1ShowURL = 7116,
                    
                    perfVSHelpWBLogTopicId = 7117,

                    
                    perfVSHelpPartnerFallBackStart = 7118, 
                    
                    perfVSHelpPartnerFallBackEnd = 7119,

                    perfVSHelpStartLoadHxSession = 7120,
                    perfVSHelpCompleteLoadHxSession = 7121,
                    perfVSHelpStartLoadHxCollection = 7122,
                    perfVSHelpCompleteLoadHxCollection = 7123,
                    perfVSHelpStartLoadHxIndex = 7124,
                    perfVSHelpCompleteLoadHxIndex = 7125,
                    perfVSHelpStartLoadHxTOC = 7126,
                    perfVSHelpCompleteLoadHxTOC = 7127,
                    perfVSHelpStartLoadHxFIndex = 7128,
                    perfVSHelpCompleteLoadHxFIndex = 7129,
                    perfVSHelpStartLoadHxKIndex = 7130,
                    perfVSHelpCompleteLoadHxKIndex = 7131,
                    perfVSHelpStartLoadHxAIndex = 7132,
                    perfVSHelpCompleteLoadHxAIndex = 7133,

                    perfVSHelpStartLocalSearch = 7141,
                    perfVSHelpStartHHQuery = 7142,
                    perfVSHelpCompleteHHQuery = 7143,
                    perfVSHelpCompleteLocalSearch = 7144,

                    perfHxInitializeSession = 7160,
                    perfHxCollectionCreated = 7161,
                    perfHxCollectionFileLoaded = 7162,
                    perfHxExCollectionLoaded = 7163,
                    perfHxCollectionInitialized = 7164,
                    perfHxExCollectionStartInit = 7165,
                    perfHxExCollNSpaceListInit = 7166,
                    perfHxExCollCtrlNSpaceInit = 7167,
                    perfHxExCollNSpaceInit = 7168,
                    perfHxExCollNSpaceCollLoad = 7169,
                    perfHxExCollTitleListBuilt = 7170,
                    perfHxExCollTopicsCounted = 7171,
                    perfHxExCollGotTitleInfo = 7172,
                    perfHxExCollMergeValidated = 7173,
                    perfHxExCollInitFTSKeyword = 7174,
                    perfHxExCollBTLStart = 7175,
                    perfHxExCollBTLBuiltFileList = 7176,
                    perfHxExCollBTLValidatedColl = 7177,
                    perfHxExCollBTLHelpFileChanged = 7178,
                    perfHxExCollBTLGotHelpFilesInfo = 7179,
                    perfHxExCollBTLValidatedFastInfo = 7180,
                    perfHxExCollBTLPersistedValidation = 7181,
                    perfHxExCollBTLHelpFileNotChanged = 7182,
                    perfHxExCollBTLPulledFastInfoData = 7183,

                    perfVSPackageSetSiteBegin = 7184,
                    perfVSPackageSetSiteEnd = 7185,

                    perfVSEditorAfterStatementCompletionPaint = 7186,
                    perfVSEditorCodeDefWindowUpdated = 7187,

                    perfVSPackageInstantiate = 7188,
                    perfVSPackageLoadBegin = 7189,
                    perfVSPackageLoadEnd = 7190,

                    perfVSPrimeEditorBegin = 7191,
                    perfVSPrimeEditorEnd = 7192,
                    perfVSPrimeEditorNotScheduled = 7193,

                    
                    perfVSGarbageCollectCLRBegin = 7194,
                    
                    perfVSGarbageCollectCLREnd = 7195,
                    
                    perfVSToolboxLoaded = 7196,
                    
                    perfVSTshellScriptDone = 7197,
                    
                    perfVSProfilerAttached = 7198,
                    
                    perfVSClientRunStart = 7199,

                    
                    perfVBCompilerPrettyListBegin = 7200,
                    perfVBCompilerPrettyListEnd = 7201,
                    perfVBCompilerStartOutliningBegin = 7202,
                    perfVBCompilerStartOutliningEnd = 7203,
                    perfVBCompilerUpdateLineSeparatorsBegin = 7204,
                    perfVBCompilerUpdateLineSeparatorsEnd = 7205,
                    perfVBCompilerEditClassifyBegin = 7206,
                    perfVBCompilerEditClassifyEnd = 7207,
                    perfVBCompilerEditFilterBegin = 7208,
                    perfVBCompilerEditFilterEnd = 7209,
                    perfVBCompilerSymbolLocationUpdateBegin = 7210,
                    perfVBCompilerSymbolLocationUpdateEnd = 7211,
                    perfVBCompilerBackgroundThreadStop = 7212,
                    perfVBCompilerBackgroundThreadStart = 7213,
                    perfVBCompilerCodeModelLoadFileBegin = 7214,
                    perfVBCompilerCodeModelLoadFileEnd = 7215,
                    perfVBCompilerDropDownLoadBegin = 7216,
                    perfVBCompilerDropDownLoadEnd = 7217,
                    perfVBCompilerClassViewObjectRefreshBegin = 7218,
                    perfVBCompilerClassViewObjectRefreshEnd = 7219,
                    perfVBCompilerIntellisenseBegin = 7220,
                    perfVBCompilerIntellisenseEnd = 7221,
                    perfVBCompilerReachedBoundState = 7222,
                    perfVBCompilerReachedCompiledState = 7223,
                    perfVBCompilerCompilationAborted = 7224,
                    perfVBCompilerFileChanged = 7225,
                    perfVBDebuggerENCDeltaGenBegin = 7226,
                    perfVBDebuggerENCDeltaGenEnd = 7227,
                    perfVBDebuggerENCEnterBreak = 7228,
                    perfVBDebuggerENCExitBreak = 7229,
                    perfVBCompilerRegisterDesignViewAttributeBegin = 7230,
                    perfVBCompilerRegisterDesignViewAttributeEnd = 7231,
                    perfVBCompilerCommitBegin = 7232,
                    perfVBCompilerCommitEnd = 7233,
                    perfVBMakeTypeCorrectionBegin = 7234,
                    perfVBMakeTypeCorrectionEnd = 7235,
                    perfVBRenameSymbolEnd = 7236,
                    perfVBIntelliXMLIndexingEnd = 7237,
                    perfVBInsertSnippetEnd = 7238,
                    perfVBNavigateToStartSearch = 7239,
                    perfVBNavigateToEndSearch = 7240,
                    perfVBSmartTagInitializeFixesBegin = 7241,
                    perfVBSmartTagInitializeFixesEnd = 7242,
                    perfVBApplyQuickFixBegin = 7243,
                    perfVBApplyQuickFixEnd = 7244,
                    perfVBCompilerBackgroundThreadFinishedStopping = 7245,
                    perfVBClassificationBegin = 7246,
                    perfVBClassificationEnd = 7247,
                    perfVBTypeClassificationBegin = 7248,
                    perfVBTypeClassificationEnd = 7249,
                    perfVBRefreshTaskListBegin = 7250,
                    perfVBRefreshTaskListEnd = 7251,
                    perfVBIdleSyncDropDownsBegin = 7252,
                    perfVBIdleSyncDropDownsEnd = 7253,
                    perfVBIdleProcessXMLSchemasBegin = 7254,
                    perfVBIdleProcessXMLSchemasEnd = 7255,
                    perfVBIdleProcessHighlightReferencesBegin = 7256,
                    perfVBIdleProcessHighlightReferencesEnd = 7257,
                    perfVBIdleProcessManagedTaskBegin = 7258,
                    perfVBIdleProcessManagedTaskEnd = 7259,
                    perfVBIdleProcessFileChangeBegin = 7260,
                    perfVBIdleProcessFileChangeEnd = 7261,
                    perfVBFindAllReferencesBegin = 7262,
                    perfVBFindAllReferencesEnd = 7263,
                    perfVBReferenceSearchBegin = 7264,
                    perfVBReferenceSearchEnd = 7265,

                    
                    perfViewSwitchBegin = 7300,
                    perfViewSwitchEnd = 7301,
                    perfParseBegin = 7302,
                    perfParseEnd = 7303,
                    perfSecondaryBufferCodeGenerationBegin = 7304,
                    perfSecondaryBufferCodeGenerationEnd = 7305,
                    perfIntellisenseWindowPopulationBegin = 7306,
                    perfIntellisenseWindowPopulationEnd = 7307,
                    perfSchemaLoadBegin = 7308,
                    perfSchemaLoadEnd = 7309,
                    perfValidationBegin = 7310,
                    perfValidationEnd = 7311,
                    perfEventHandlerGenerationEnd = 7312,
                    perfSCPEnd = 7313,

                    
                    perfEditorReady = 7314,
                    perfEventHandlerGenerationBegin = 7315,
                    perfEditorStartupBegin = 7316,
                    perfEditorStartupEnd = 7317,
                    
                    perfWebFormTagIntellisenseReady = 7318,
                    
                    perfWebFormCodeIntellisenseReady = 7319,
                    
                    qaTaskListReady = 7320,
                    
                    qaMarkupOutlineReady = 7321,
                    perfWebFormEventNavigationBegin = 7322,
                    perfWebFormEventNavigationEnd = 7323,
                    perfWebFormLoadComplete = 7324,
                    perfWebFormFirstIdleInView = 7325,

                    
                    perfIntellisenseParseBegin = 7326,
                    perfIntellisenseParseEnd = 7327,

                    
                    perfViewSyncBegin = 7328,
                    
                    perfViewSyncEnd = 7329,

                    
                    qaEditorIdleProcessingDone = 7330,
                    
                    perfSelectionSyncBegin = 7331,
                    
                    perfSelectionSyncEnds = 7332,

                    perfWebFormExternalTLBGenerationRequest = 7333,
                    perfWebFormExternalTLBReady = 7334,

                    
                    perfPropertiesWindowUpdateBegin = 7335,
                    perfPropertiesWindowUpdateEnd = 7336,

                    
                    perfIdleLoopBegin = 7337,
                    perfIdleLoopEnd = 7338,

                    perfWebFormLoadBegin = 7339,
                    perfWebEditorScreenValidationBegin = 7340,
                    perfWebEditorScreenValidationEnd = 7341,

                    
                    perfVCDTParseOnMainThreadBegin = 7350,
                    perfVCDTParseOnParserThreadBegin = 7351,
                    perfVCDTParseEnd = 7352,
                    perfVCDTParseAbort = 7353,
                    perfVCDTOutlineBegin = 7354,
                    perfVCDTOutlineEnd = 7355,
                    qaVCDTStoreExplicitFile = 7357,
                    qaVCDTStoreImplicitFile = 7358,
                    perfVCDTCodeStoreInitialized = 7359,
                    perfVCDTErrorUpdateQueued = 7360,
                    perfVCDTErrorUpdateBegin = 7361,
                    perfVCDTErrorUpdateEnd = 7362,
                    perfVCDTAllConfigurationsInitialized = 7363,
                    perfVCDTIfDefUpdateQueued = 7364,
                    perfVCDTIfDefUpdateBegin = 7365,
                    perfVCDTIfDefUpdateEnd = 7366,
                    perfVCDTRefLookupConsumed = 7367,
                    perfVCDTNavigateToStartSearch = 7368,
                    perfVCDTNavigateToEndSearch = 7369,
                    perfVCDTGotoDef = 7370,
                    perfVCDTGotoDecl = 7371,
                    perfVCDTFAR = 7372,
                    perfVCDTQuickInfo = 7373,
                    perfVCDTMemberList = 7374,
                    perfVCDTParamHelp = 7375,
                    perfVCDTPreParse = 7376,
                    perfVCDTAutoComplete = 7377,
                    perfVCDTUpdateRange = 7378,
                    perfVCDTUpdateRangeTotal = 7379,
                    perfVCDTFilesInitialized = 7380,
                    perfVCDTRefLookupBegin = 7381,
                    perfVCDTRefLookupEnd = 7382,
                    perfVCDTRefLookupItemResolved = 7383,
                    perfVCDTCallHierarchy = 7384,
                    perfVCDTCallsFrom = 7385,
                    perfVCDTIntelliSenseOperationFailed = 7386,


                    
                    perfVSProjShowCodeBegin = 7400,
                    perfVSProjShowCodeEnd = 7401,
                    perfVSProjShowDesignerBegin = 7402,
                    perfVSProjShowDesignerEnd = 7403,
                    perfVSProjFactoryCreateProjectBegin = 7404,
                    perfVSProjFactoryCreateProjectEnd = 7405,
                    perfVSProjCreateProjectBegin = 7406,
                    perfVSProjCreateProjectEnd = 7407,
                    perfVSProjLoadProjectFileBegin = 7408,
                    perfVSProjLoadProjectFileEnd = 7409,

                    perfVSProjPublishBegin = 7410,
                    perfVSProjPublishEnd = 7411,

                    perfVSProjLoadMSBuildProjectFileBegin = 7412,
                    perfVSProjLoadMSBuildProjectFileEnd = 7413,

                    perfVSProjSetCmdUIContextBegin = 7414,
                    perfVSProjSetCmdUIContextEnd = 7415,
                    perfVSSolutionOnAfterOpenSolutionBegin = 7416,
                    perfVSSolutionOnAfterOpenSolutionEnd = 7417,
                    perfVSProjPOGRefreshBegin = 7418,
                    perfVSProjPOGRefreshEnd = 7419,

                    
                    perfVSProjOnAfterManagedProjectCreate = 7420,
                    
                    perfVSProjOnStartHostingProcess = 7421,
                    
                    perfVSProjOnHostingProcessNotUsed = 7422,

                    perfEditorReplaceInFilesStart = 7423,
                    perfEditorReplaceInFilesEnd = 7424,
                    perfEditorPaintStart = 7425,
                    perfEditorPaintEnd = 7426,
                    perfEditorLoadTextImageFromMemoryStart = 7427,
                    perfEditorLoadTextImageFromMemoryEnd = 7428,
                    perfEditorSaveTextImageToMemoryStart = 7429,
                    perfEditorSaveTextImageToMemoryEnd = 7430,
                    perfEditorSaveTextReplaceLinesExStart = 7431,
                    perfEditorSaveTextReplaceLinesExEnd = 7432,
                    perfEditorSaveTextReplaceStreamExStart = 7433,
                    perfEditorSaveTextReplaceStreamExEnd = 7434,
                    perfEditorSaveTextVerticalScrollStart = 7435,
                    perfEditorSaveTextVerticalScrollEnd = 7436,
                    perfEditorCreateEditorInstance = 7437,

                    perfAddRefDlgDNPopulateThreadBegin = 7438,
                    perfAddRefDlgDNPopulateThreadEnd = 7439,
                    perfAddRefDlgDNEnumerateAssembliesBegin = 7440,
                    perfAddRefDlgDNEnumerateAssembliesEnd = 7441,
                    perfAddRefDlgDNFilteringBegin = 7442,
                    perfAddRefDlgDNFilteringEnd = 7443,
                    perfVSCloseSolutionBegin = 7444,

                    
                    perfVSFirstLaunchSetupBegin = 7445,
                    
                    perfVSFirstLaunchSetupEnd = 7446,

                    
                    perfVSSolutionBackgroundSolutionLoadComplete = 7447,

                    perfAddRefDlgChangeReferencesBegin = 7448,
                    perfAddRefDlgChangeReferencesEnd = 7449,

                    
                    perfVSWebMigrationBegin = 7450,
                    perfVSWebMigrationEnd = 7451,
                    perfVSWebAfterFirstIdle = 7452,
                    perfVSWebOpenStarts = 7453,
                    perfVSWebOpenEnds = 7454,
                    perfVSWebInitialProcessingComplete = 7455,
                    perfVSWebBuildWebsiteBegins = 7456,
                    perfVSWebBuildWebsiteEnds = 7457,
                    perfVSWebCodeMarkerControl = 7458,
                    perfViewInBrowserEnd = 7459,
                    perfWebDeploymentBegins = 7460,
                    perfWebDeploymentEnds = 7461,

                    perfCompileBegin = 7470,
                    perfCompileEnd = 7471,

#endif 

                    
#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeFXMarkers)
                    
                    perfFXDesignCreateComponentEnd = 7501,
                    perfFXDesignPropertyBrowserPopulationStart = 7502,
                    perfFXDesignPropertyBrowserPopulationEnd = 7503,
                    perfFXDesignShowCode = 7505,
                    perfFXDesignFromCodeToDesignStart = 7506,
                    perfFXDesignFromCodeToDesign = 7507,
                    perfFXDesignFlushStart = 7508,
                    perfFXDesignFlushEnd = 7509,
                    perfFXBindEventDesignToCode = 7511,
                    perfFXGenerateCodeTreeEnd = 7513,
                    perfFXIntegrateSerializedTreeEnd = 7515,
                    perfFXOnLoadedStart = 7516,
                    perfFXOnLoadedEnd = 7517,
                    perfFXCreateEditorStart = 7518,
                    perfFXCreateEditorEnd = 7519,
                    perfFXParseEnd = 7520,
                    perfFXPerformLoadStart = 7521,
                    perfFXPerformLoadEnd = 7522,
                    perfFXEmitMethodEnd = 7523,
                    perfFXFormatMethodEnd = 7524,
                    perfFXCodeGenerationEnd = 7525,
                    perfFXGetDocumentType = 7526,
                    perfFXDeserializeStart = 7527,
                    perfFXDeserializeEnd = 7528,
                    perfFXGetFileDocDataStart = 7529,
                    perfFXGetFileDocDataEnd = 7530,
                    perfFXCreateDesignerStart = 7531,
                    perfFXCreateDesignerEnd = 7532,
                    perfFXCreateDesignSurface = 7533,
                    perfFXCreateDesignSurfaceEnd = 7534,

                    perfFXNotifyStartupServices = 7535,
                    perfFXNotifyStartupServicesEnd = 7536,
                    perfFXGetGlobalObjects = 7537,
                    perfFXGetGlobalObjectsEnd = 7538,
                    perfFXDesignPropertyBrowserCreate = 7539,
                    perfFXDesignPropertyBrowserCreateEnd = 7540,
                    perfFXDesignPropertyBrowserLoadState = 7541,
                    perfFXDesignPBOnSelectionChanged = 7542,
                    perfFXDesignPBOnSelectionChangedEnd = 7543,
                    perfFXDesignElementHostDesignerSetChildEnd = 7544,

                    perfFXDiscoverDotNetComponentToolboxItemStart = 7545,
                    perfFXDiscoverDotNetComponentToolboxItemEnd = 7546,
                    
                    perfFXToolboxRefreshEnd = 7547,

                    
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSCoreMarkers)
                    
                    
                    perfHelpPageLoadComplete = 7600,
                    
                    perfHelpSearchResultsRendered = 7601,
                    
                    perfHelpTaskCategoryRendered = 7602,
                    
                    perfHelpPageConstructorCalled = 7603,
                    
                    perfHelpPageLoadStart = 7604,

                    
                    perfVSWebHandlerStart = 7605,

                    
                    perfVSWebHandlerEnd = 7606,

                    
                    perfVSWebLauncherStart = 7607,

                    
                    perfVSWebLauncherEnd = 7608,
                    
                    perfVSHelpStartSearch = 7610,
                    perfVSHelpCompleteSearch = 7611,

                    perfMefHostGetCatalogStart = 7620,
                    perfMefHostGetCatalogEnd = 7621,
                    perfMefHostBuildDefaultCompositionStart = 7622,
                    perfMefHostBuildDefaultCompositionEnd = 7623,
                    perfMefHostGetServiceStart = 7624,
                    perfMefHostGetServiceEnd = 7625,
                    perfMefHostGetExtensionsStart = 7626,
                    perfMefHostGetExtensionsEnd = 7627,
                    perfMefHostReadCacheRecordStart = 7628,
                    perfMefHostReadCacheRecordEnd = 7629,
                    perfMefHostGetComponentAssembliesStart = 7630,
                    perfMefHostGetComponentAssembliesEnd = 7631,

                    perfBaselinePackageLoad = 7650,
                    perfBaselineServiceStart = 7651,
                    perfBaselineShowToolWindow = 7652,
                    perfBaselineToolWindowShown = 7653,
                    
                    perfVSStartPageFinishedRSSDownloading = 7654,
                    
                    perfVSStartPageStartedRSSDownloading = 7655,
                    
                    perfVSThemeImageListBegin = 7656,

                    
                    perfVSThemeImageListEnd = 7657,

                    
                    perfVSThemeDIBitsBegin = 7658,

                    
                    perfVSThemeDIBitsEnd = 7659,

                    
                    perfExtensionManagerExtensionsDeletionStart = 7668,
                    perfExtensionManagerExtensionsDeletionEnd = 7669,

                    perfExtensionManagerDialogLoaded = 7670,
                    perfExtensionManagerDialogClosed = 7671,
                    perfExtensionManagerInstallConfirmationDialogLoaded = 7672,
                    perfExtensionManagerInstallConfirmationDialogClosed = 7673,
                    perfExtensionManagerDependencyWarningDialogLoaded = 7674,
                    perfExtensionManagerDependencyWarningDialogClosed = 7675,
                    perfExtensionManagerSwitchedProvider = 7676,
                    perfExtensionManagerCategoriesRetrieved = 7677,
                    perfExtensionManagerSwitchedCategory = 7678,
                    perfExtensionManagerQueryCompleted = 7679,
                    perfExtensionManagerInstallUICompleted = 7680,
                    perfExtensionManagerProgressDialogLoaded = 7681,
                    perfExtensionManagerProgressDialogClosed = 7682,
                    perfExtensionManagerInstallStart = 7683,
                    perfExtensionManagerInstallEnd = 7684,
                    perfExtensionManagerUninstallStart = 7685,
                    perfExtensionManagerUninstallEnd = 7686,
                    perfExtensionManagerEnableStart = 7687,
                    perfExtensionManagerEnableEnd = 7688,
                    perfExtensionManagerDisableStart = 7689,
                    perfExtensionManagerDisableEnd = 7690,
                    perfExtensionManagerInitializeStart = 7691,
                    perfExtensionManagerInitializeEnd = 7692,
                    perfExtensionManagerGetEnabledExtensionContentLocationsStart = 7693,
                    perfExtensionManagerGetEnabledExtensionContentLocationsEnd = 7694,
                    perfExtensionManagerTryGetInstalledExtensionStart = 7695,
                    perfExtensionManagerTryGetInstalledExtensionEnd = 7696,

                    
                    perfVSFileChangeProcessChangeNotificationsBegin = 7697,

                    
                    perfVSFileChangeProcessChangeNotificationsEnd = 7698,

                    perfFileNamesNavigateToStartSearch = 7660,
                    perfFileNamesNavigateToEndSearch = 7699,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeSCCIMarkers)
                    
                    perfVSSCCIAddToSCCEnd = 7700,
                    perfVSSCCICheckinEnd = 7701,
                    perfVSSCCICheckoutEnd = 7702,
                    perfVSSCCIOpenFromSCCEnd = 7703,
                    perfVSSCCIGetLatestEnd = 7704,
                    perfVSSCCIVssProviderLoaded = 7705,
                    perfVSSCCIMSSCCIProviderLoaded = 7706,
                    perfVSSCCIRPCServiceUsed = 7707,
                    perfVSSCCISynchPartOfOpenFromSccEnd = 7708,
                    perfVSSCCIRPCServiceCallFailed = 7709,
                    perfVSSCCICheckoutBegin = 7710,
                    perfVSSCCICheckoutOnEditBegin = 7711,
                    perfVSSCCICheckoutOnEditEnd = 7712,
                    perfVSSCCICheckoutOnSaveBegin = 7713,
                    perfVSSCCICheckoutOnSaveEnd = 7714,
                    perfVSSCCICheckinBegin = 7715,
                    perfVSSCCIGetLatestBegin = 7716,
                    perfVSSCCIOpenFromSCCBegin = 7717,
                    perfVSSCCISynchPartOfOpenFromSccBegin = 7718,
                    perfVSSCCIAddToSCCBegin = 7719,
                    perfVSSCCIGetBegin = 7720,
                    perfVSSCCIGetEnd = 7721,
                    perfVSSCCIAsynchStatusUpdateComplete = 7722,
                    perfVSSCCIRefreshStatusEnd = 7723,
                    perfVSSCCIRefreshStatusBegin = 7724,

                    perfVSSCCIPrepForDiffBegin = 7726,
                    perfVSSCCIPrepForDiffEnd = 7727,
                    perfVSSCCIBrowseForBindBegin = 7728,
                    perfVSSCCIApplyBindBegin = 7729,
                    perfVSSCCIBrowseForBindEnd = 7730,
                    perfVSSCCIApplyBindEnd = 7731,
                    perfVSSCCIUndoEnd = 7732,
                    perfVSSCCIUndoBegin = 7733,
                    perfVSSCCIQueryRenameBegin = 7734,
                    perfVSSCCIQueryRenameEnd = 7735,
                    perfVSSCCIQueryRemoveBegin = 7736,
                    perfVSSCCIQueryRemoveEnd = 7737,
                    perfVSSCCIOnAfterRenameBegin = 7738,
                    perfVSSCCIOnAfterRenameEnd = 7739,
                    perfVSSCCIOnAfterRemoveBegin = 7740,
                    perfVSSCCIOnAfterRemoveEnd = 7741,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSSMarkers)
                    
                    perfVssSSExpStartupEnd = 7750,
                    perfVssSSExpUpdateChildrenEnd = 7751,
                    perfVssSSExpStatusSearchEnd = 7752,
                    perfVssSSExpWildcardSearchEnd = 7753,
                    perfVssSSExpRefreshFileListEnd = 7754,
                    perfVssMsscciInitializeBegin = 7755,
                    perfVssMsscciInitializeEnd = 7756,
                    perfVssMsscciUninitializeBegin = 7757,
                    perfVssMsscciUninitializeEnd = 7758,
                    perfVssMsscciOpenProjectBegin = 7759,
                    perfVssMsscciOpenProjectEnd = 7760,
                    perfVssMsscciCloseProjectBegin = 7761,
                    perfVssMsscciCloseProjectEnd = 7762,
                    perfVssMsscciCheckoutBegin = 7763,
                    perfVssMsscciCheckoutEnd = 7764,
                    perfVssMsscciUncheckoutBegin = 7765,
                    perfVssMsscciUncheckoutEnd = 7766,
                    perfVssMsscciCheckinBegin = 7767,
                    perfVssMsscciCheckinEnd = 7768,
                    perfVssMsscciGetBegin = 7769,
                    perfVssMsscciGetEnd = 7770,
                    perfVssMsscciPopulateListBegin = 7771,
                    perfVssMsscciPopulateListEnd = 7772,
                    perfVssMsscciQueryChangesBegin = 7773,
                    perfVssMsscciQueryChangesEnd = 7774,
                    perfVssMsscciEnumChangedFilesBegin = 7775,
                    perfVssMsscciEnumChangedFilesEnd = 7776,
                    perfVssMsscciPopulateDirListBegin = 7777,
                    perfVssMsscciPopulateDirListEnd = 7778,
                    perfVssSSExpCheckOut = 7779,
                    perfVssSSExpCheckIn = 7780,
                    perfVssUtilsArchiveStart = 7781,
                    perfVssUtilsArchiveEnd = 7782,
                    perfVssUtilsRestoreStart = 7783,
                    perfVssUtilsRestoreEnd = 7784,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVenusMarkers)
                    
                    perfEditActivateRegion = 7800,
                    perfControlHostingInitialized = 7801,
                    perfControlHostingLoadComplete = 7802,
                    perfControlHostingProxyRenderComplete = 7803,
                    perfControlHostingProxyAssembliesReady = 7804,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeTrinityMarkers)
                    
                    
                    perfVSTOVSTProjectPackageSetSiteStart = 8000,
                    perfVSTOVSTProjectPrecreateForOuterStart = 8001,
                    perfVSTOVSTProjectSetInnerProjectEnd = 8002,
                    perfVSTOVSTProjectInitializeForOuterStart = 8003,
                    perfVSTOVSTProjectInitializeForOuterEnd = 8004,
                    perfVSTOVSTProjectSyncWithHostStart = 8005,
                    perfVSTOVSTProjectSyncWithHostEnd = 8006,
                    perfVSTOVSTProjectSetProjectClientStart = 8007,
                    perfVSTOVSTProjectSetProjectClientEnd = 8008,
                    perfVSTOVSTProjectOnProjectCreatedStart = 8009,
                    perfVSTOVSTProjectOnProjectCreatedEnd = 8010,
                    perfVSTOVSTProjectAddExtensibleItemStart = 8011,
                    perfVSTOVSTProjectAddExtensibleItemEnd = 8012,
                    perfVSTOVSTProjectRefreshBufferContentStart = 8013,
                    perfVSTOVSTProjectRefreshBufferContentEnd = 8014,

                    
                    perfVSTOVSTInteractiveProjectCreateStart = 8015,
                    perfVSTOVSTInteractiveProjectCreateEnd = 8016,
                    perfVSTOVSTInteractiveProjectResetAllStart = 8017,
                    perfVSTOVSTInteractiveProjectResetAllEnd = 8018,

                    perfVSTOVSTProjectWizard = 8019,
                    perfVSTOVSTProjectWizardOnFinish = 8020,
                    perfVSTOVSTProjectWizardOnBeforeCreateProjectStart = 8021,
                    perfVSTOVSTProjectWizardOnBeforeCreateProjectEnd = 8022,
                    perfVSTOVSTProjectWizardProjectFinishedGeneratingStart = 8023,
                    perfVSTOVSTProjectWizardProjectFinishedGeneratingEnd = 8024,
                    perfVSTOVSTProjectBlueprintInitStart = 8025,
                    perfVSTOVSTProjectBlueprintInitEnd = 8026,


                    
                    perfVSTOVSTDesignerCreateLoaderStart = 8028,
                    perfVSTOVSTDesignerCreateLoaderEnd = 8029,
                    perfVSTOVSTDesignerDoVerbStart = 8030,
                    perfVSTOVSTDesignerDoVerbEnd = 8031,
                    perfVSTOVSTDesignerSetSiteStart = 8032,
                    perfVSTOVSTDesignerSetSiteEnd = 8033,
                    perfVSTOVSTDesignerInitDocDesignerStart = 8034,
                    perfVSTOVSTDesignerInitDocDesignerEnd = 8035,
                    perfVSTOVSTDesignerOnViewChangedStart = 8036,
                    perfVSTOVSTDesignerOnViewChangedEnd = 8037,
                    perfVSTOVSTDesignerCoCreateStart = 8038,
                    perfVSTOVSTDesignerCoCreateEnd = 8039,
                    perfVSTOVSTDesignerBeginLoadStart = 8040,
                    perfVSTOVSTDesignerBeginLoadEnd = 8041,

                    
                    perfVSTOVSTDSWDropStart = 8042,
                    perfVSTOVSTDSWDropEnd = 8043,

                    
                    perfVSTOVSTClientUpdateProjectStart = 8044,
                    perfVSTOVSTClientUpdateProjectEnd = 8045,
                    perfVSTOVSTClientHostSideAdaptorStart = 8046,
                    perfVSTOVSTClientHostSideAdaptorEnd = 8047,
                    perfVSTOVSTClientOleRunStart = 8048,
                    perfVSTOVSTClientOleRunEnd = 8049,
                    perfVSTOVSTClientInstantiateDocumentStart = 8050,
                    perfVSTOVSTClientInstantiateDocumentEnd = 8051,
                    perfVSTOVSTClientRefreshProgrammingModelStart = 8052,
                    perfVSTOVSTClientRefreshProgrammingModelEnd = 8053,
                    perfVSTOVSTClientShowDocumentStart = 8054,
                    perfVSTOVSTClientShowDocumentEnd = 8055,
                    perfVSTOVSTClientBindStart = 8056,
                    perfVSTOVSTClientBindEnd = 8057,
                    perfVSTOVSTClientCoCreateHostInstanceStart = 8058,
                    perfVSTOVSTClientCoCreateHostInstanceEnd = 8059,


                    
                    perfVSTOAppInfoTaskStart = 8060,
                    perfVSTOAppInfoTaskEnd = 8061,
                    perfVSTOCustomizeFirstStart = 8062,
                    perfVSTOCustomizeFirstEnd = 8063,
                    perfVSTOCustomizeStartupInfoStart = 8064,
                    perfVSTOCustomizeStartupInfoEnd = 8065,
                    perfVSTOCustomizeAppAsmDependStart = 8066,
                    perfVSTOCustomizeAppAsmDependEnd = 8067,
                    perfVSTOCustomizeRefAsmDependStart = 8068,
                    perfVSTOCustomizeRefAsmDependEnd = 8069,
                    perfVSTOCustomizeEntryPointsStart = 8070,
                    perfVSTOCustomizeEntryPointsEnd = 8071,
                    perfVSTOCustomizePersistStart = 8072,
                    perfVSTOCustomizePersistEnd = 8073,
                    perfVSTOCustomizeLastStart = 8074,
                    perfVSTOCustomizeLastEnd = 8075,

                    perfVSTOPersisterGetObjectStart = 8076,
                    perfVSTOPersisterGetObjectEnd = 8077,
                    perfVSTORibbonGetCustomUIStart = 8078,
                    perfVSTORibbonGetCustomUIEnd = 8079,
                    perfVSTOPersisterWriteStart = 8080,
                    perfVSTOPersisterWriteEnd = 8081,
                    perfVSTOPersisterFinishedStart = 8082,
                    perfVSTOPersisterFinishedEnd = 8083,

                    perfVSTOOfficePersistenceObjectIsOpenedStart = 8084,
                    perfVSTOOfficePersistenceObjectIsOpenedEnd = 8085,
                    perfVSTOOfficePersistenceObjectCoCreateStart = 8086,
                    perfVSTOOfficePersistenceObjectCoCreateEnd = 8087,
                    perfVSTOOfficePersistenceObjectDeletePropsStart = 8088,
                    perfVSTOOfficePersistenceObjectDeletePropsEnd = 8089,
                    perfVSTOOfficePersistenceObjectOpenDocStart = 8090,
                    perfVSTOOfficePersistenceObjectOpenDocEnd = 8091,
                    perfVSTOOfficePersistenceObjectAddCtrlStart = 8092,
                    perfVSTOOfficePersistenceObjectAddCtrlEnd = 8093,
                    perfVSTOOfficePersistenceObjectSetProtectionStart = 8094,
                    perfVSTOOfficePersistenceObjectSetProtectionEnd = 8095,

                    perfVSTOVSTSecurityTaskStart = 8096,
                    perfVSTOVSTSecurityTaskEnd = 8097,

                    perfVSTOCustomizeDataCacheStart = 8098,
                    perfVSTOCustomizeDataCacheEnd = 8099,

                    

                    perfVSTOReadCachedDataManifestStart = 8100,
                    perfVSTOReadCachedDataManifestEnd = 8101,
                    perfVSTOGetRibbonObjectStart = 8102,
                    perfVSTOGetRibbonObjectEnd = 8103,
                    perfVSTOOpenEventStart = 8104,
                    perfVSTOOpenEventEnd = 8105,
                    perfVSTOFindControlStart = 8106,
                    perfVSTOFindControlEnd = 8107,


                    perfVSTOCreateEvidenceStart = 8110,
                    perfVSTOCreateEvidenceEnd = 8111,


                    perfStartCLRStart = 8114,
                    perfStartCLREnd = 8115,
                    perfVSTOCreateDomainStart = 8116,
                    perfVSTOCreateDomainEnd = 8117,


                    perfVSTOLoadRuntimeAssemblyStart = 8120,
                    perfVSTOLoadRuntimeAssemblyEnd = 8121,
                    perfVSTOExecManifestStart = 8122,
                    perfVSTOExecManifestEnd = 8123,


                    perfVSTOExecManifestParseManifestStart = 8126,
                    perfVSTOExecManifestParseManifestEnd = 8127,
                    perfVSTOExecManifestUpdateStart = 8128,
                    perfVSTOExecManifestUpdateEnd = 8129,
                    perfVSTOExecManifestSetPolicyStart = 8130,
                    perfVSTOExecManifestSetPolicyEnd = 8131,
                    perfVSTOExecManifestConfigStart = 8132,
                    perfVSTOExecManifestConfigEnd = 8133,


                    perfVSTOExecManifestConfigLoadStartupAsmStart = 8136,
                    perfVSTOExecManifestConfigLoadStartupAsmEnd = 8137,
                    perfVSTOExecManifestConfigCreateStartupObjAsmGetTypeStart = 8138,
                    perfVSTOExecManifestConfigCreateStartupObjAsmGetTypeEnd = 8139,



                    perfVSTOExecManifestConfigCreateStartupObjInvokeStart = 8142,
                    perfVSTOExecManifestConfigCreateStartupObjInvokeEnd = 8143,
                    perfVSTOExecManifestCompleteStartupObjectInitializationStart = 8144,
                    perfVSTOExecManifestCompleteStartupObjectInitializationEnd = 8145,
                    perfVSTOCreateForClientNewStart = 8146,
                    perfVSTOCreateForClientNewEnd = 8147,
                    perfVSTOCreateForClientStartupStart = 8148,
                    perfVSTOCreateForClientStartupEnd = 8149,
                    perfVSTOCreateForClientMyAppStart = 8150,
                    perfVSTOCreateForClientMyAppEnd = 8151,
                    perfVSTOCreateForClientInitializeViewComponentsStart = 8152,
                    perfVSTOCreateForClientInitializeViewComponentsEnd = 8153,
                    perfVSTOCreateForClientInitializeComponentsStart = 8154,
                    perfVSTOCreateForClientInitializeComponentsEnd = 8155,
                    perfVSTOCreateForClientInitializeDataComponentsStart = 8156,
                    perfVSTOCreateForClientInitializeDataComponentsEnd = 8157,
                    perfVSTOCreateForClientBindStart = 8158,
                    perfVSTOCreateForClientBindEnd = 8159,
                    perfVSTOCreateForClientInitCompleteStart = 8160,
                    perfVSTOCreateForClientInitCompleteEnd = 8161,
                    perfVSTOLoaderLoadStart = 8162,
                    perfVSTOLoaderLoadEnd = 8163,
                    perfVSTOBootStart = 8164,
                    perfVSTOBootEnd = 8165,





                    perfVSTOGetDefaultDomainStart = 8168,
                    perfVSTOGetDefaultDomainEnd = 8169,
                    perfVSTOGetAppbaseStart = 8170,
                    perfVSTOGetAppbaseEnd = 8171,
                    perfVSTOCreateCustomizationDomainStart = 8172,
                    perfVSTOCreateCustomizationDomainEnd = 8173,
                    perfVSTOCreateUrisStart = 8174,
                    perfVSTOCreateUrisEnd = 8175,
                    perfVSTOGetConfigStringsStart = 8176,
                    perfVSTOGetConfigStringsEnd = 8177,
                    perfVSTOReflectOnADMStart = 8178,
                    perfVSTOReflectOnADMEnd = 8179,
                    perfVSTOCreateDictionariesStart = 8180,
                    perfVSTOCreateDictionariesEnd = 8181,
                    perfVSTOCallbackHostStart = 8182,
                    perfVSTOCallbackHostEnd = 8183,
                    perfVSTOCompleteInitializationStart = 8184,
                    perfVSTOCompleteInitializationEnd = 8185,

                    
                    perfSharePointProjectWakeSharePointStart = 8186,
                    perfSharePointProjectWakeSharePointEnd = 8187,
                    perfSharePointProjectDeployStart = 8188,
                    perfSharePointProjectDeployEnd = 8189,
                    perfSharePointProjectAttachDebuggerStart = 8190,
                    perfSharePointProjectAttachDebuggerEnd = 8191,
                    perfSharePointProjectLoadEnd = 8192,
                    perfSharePointWizardFinishStart = 8193,
                    perfSharePointWizardFinishEnd = 8194,
                    perfSharePointDesignerLoadDesignerStart = 8195,
                    perfSharePointDesignerLoadDesignerEnd = 8196,
                    perfSharePointPackageWspPackageStart = 8197,
                    perfSharePointPackageWspPackageEnd = 8198,


                    
#endif 


#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeEFTMarkers)
                    
                    
                    perfVSWhitehorseStartDragBegin = 8200,
                    
                    perfVSWhitehorseStartDragEnd = 8201,
                    
                    perfVSWhitehorseDropBegin = 8202,
                    
                    perfVSWhitehorseDropEnd = 8203,
                    
                    perfVSWhitehorsePaintBegin = 8204,
                    
                    perfVSWhitehorsePaintEnd = 8205,
                    
                    perfVSWhitehorseLoadDocumentBegin = 8206,
                    
                    perfVSWhitehorseLoadDocumentEnd = 8207,
                    
                    perfVSWhitehorseKeyDownBegin = 8208,
                    
                    perfVSWhitehorseKeyDownEnd = 8209,
                    
                    perfVSWhitehorseKeyUpBegin = 8210,
                    
                    perfVSWhitehorseKeyUpEnd = 8211,
                    
                    perfVSWhitehorseTopLevelTransactionBegin = 8212,
                    
                    perfVSWhitehorseTopLevelTransactionCommitStart = 8213,
                    
                    perfVSWhitehorseTopLevelTransactionCommitEnd = 8214,
                    
                    perfVSWhitehorseOperationDesignerLoadStart = 8215,
                    
                    perfVSWhitehorseOperationDesignerLoadEnd = 8216,
                    
                    perfVSWhitehorseOperationDesignerExpandStart = 8217,
                    
                    perfVSWhitehorseOperationDesignerExpandEnd = 8218,
                    
                    perfVSWhitehorseCodeHandlerWalkFileBegin = 8219,
                    
                    perfVSWhitehorseCodeHandlerWalkFileEnd = 8220,
                    
                    perfVSWhitehorseCodeHandlerOnChangeLineTextBegin = 8221,
                    
                    perfVSWhitehorseCodeHandlerOnChangeLineTextEnd = 8222,
                    
                    perfVSWhitehorseCodeHandlerCodeModelEventBegin = 8223,
                    
                    perfVSWhitehorseCodeHandlerCodeModelEventEnd = 8224,
                    
                    perfVSWhitehorseContextMenuBegin = 8225,
                    
                    perfVSWhitehorseContextMenuEnd = 8226,
                    
                    perfVSWhitehorseSyncBegin = 8235,
                    
                    perfVSWhitehorseSyncEnd = 8236,
                    
                    perfVSWhitehorseBackgroundSyncEnd = 8237,
                    
                    perfVSWhitehorseSCESearchBegin = 8238,
                    
                    perfVSWhitehorseSCESearchEnd = 8239,
                    perfVSWhitehorseT4CodeGenerationBegin = 8240,
                    perfVSWhitehorseT4CodeGenerationEnd = 8241,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSDMarkers)
                    
                    
                    perfVSDeployBegin = 8300,
                    
                    perfVSDeployEnd = 8301,
                    
                    perfEmulatorBootBegin = 8302,
                    
                    perfEmulatorBootEnd = 8303,
                    
                    perfVSDDataToolsDragDropBegin = 8304,
                    
                    perfVSDDataToolsDragDropEnd = 8305,
                    
                    perfVSDCodeGenerationBegin = 8306,
                    
                    perfVSDCodeGenerationEnd = 8307,
                    
                    perfVSDConnectToDeviceBegin = 8308,
                    
                    perfVSDConnectToDeviceEnd = 8309,
                    
                    perfVSDSendFilesBegin = 8310,
                    
                    perfVSDSendFilesEnd = 8311,
                    
                    perfVSDVerifyFilesInstalledBegin = 8312,
                    
                    perfVSDVerifyFilesInstalledEnd = 8313,
                    
                    perfVSDDEMSaveBegin = 8314,
                    
                    perfVSDDEMSaveEnd = 8315,
                    
                    perfVSDDEMReconfigStart = 8316,
                    
                    perfVSDDEMReconfigEnd = 8317,
                    
                    perfVSDDEMLoadDECFGStart = 8318,
                    
                    perfVSDDEMLoadDECFGEnd = 8319,
                    
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeMSVSEditorsMarkers)
                    
                    
                    perfMSVSEditorsShowTabBegin = 8400,
                    perfMSVSEditorsShowTabEnd = 8401,
                    
                    perfMSVSEditorsActivateLogicalViewStart = 8402,
                    perfMSVSEditorsActivateLogicalViewEnd = 8403,
                    perfMSVSEditorsWCFASRDialogShown = 8404,
                    perfMSVSEditorsWCFASRDialogFillingListOfServicesStart = 8405,
                    perfMSVSEditorsWCFASRDialogFillingListOfServicesEnd = 8406,
                    perfMSVSEditorsWCFASRDialogExpandingNodeStart = 8407,
                    perfMSVSEditorsWCFASRDialogExpandingNodeEnd = 8408,
                    perfMSVSEditorsWCFReferenceGroupSavedToProject = 8409,
                    perfMSVSEditorsWCFCSRDialogShown = 8410,
                    perfMSVSEditorsWCFASRDialogServicesInSolutionStart = 8411,
                    perfMSVSEditorsWCFASRDialogServicesInSolutionEnd = 8412,
                    perfMSVSEditorsReferencePageWCFAdded = 8413,
                    perfMSVSEditorsReferencePagePostponedUIRefreshDone = 8414,
                    perfMSVSEditorsWCFUpdateStart = 8415,
                    perfMSVSEditorsWCFUpdateComplete = 8416,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeCSharpMarkers)
                    
                    
                    perfVSCSharpBatchedRequestsAdded = 8500,
                    
                    perfVSCSharpBatchedRequestsCompleted = 8501,
                    perfVSCSharpGetXmlStart = 8502,
                    perfVSCSharpGetXmlEnd = 8503,
                    perfVSCSharpRenameStart = 8504,
                    perfVSCSharpRenameFindDefinitionStart = 8505,
                    perfVSCSharpRenameFindDefinitionEnd = 8506,
                    perfVSCSharpRenameFindReferencesStart = 8507,
                    perfVSCSharpRenameFindReferencesEnd = 8508,
                    perfVSCSharpRenamePreviewStart = 8509,
                    perfVSCSharpRenamePreviewEnd = 8510,
                    perfVSCSharpRenameApplyStart = 8511,
                    perfVSCSharpRenameApplyEnd = 8512,
                    perfVSCSharpRenameVerifyStart = 8513,
                    perfVSCSharpRenameVerifyEnd = 8514,
                    perfVSCSharpRenameEnd = 8515,
                    perfVSCSharpCompletionListStart = 8516,
                    perfVSCSharpCompletionListEnd = 8517,
                    perfVSCSharpQuickInfoStart = 8518,
                    perfVSCSharpQuickInfoEnd = 8519,
                    perfVSCSharpParamHelpStart = 8520,
                    perfVSCSharpParamHelpEnd = 8521,
                    perfVSCSharpFormatStart = 8522,
                    perfVSCSharpFormatEnd = 8523,
                    perfVSCSharpCommitStart = 8524,
                    perfVSCSharpCommitEnd = 8525,
                    perfVSCSharpRenameDialogActivated = 8526,
                    perfVSCSharpNavigateToStartSearch = 8527,
                    perfVSCSharpNavigateToEndSearch = 8528,
                    perfVSCSharpGenerateTypeNoUIStart = 8529,
                    perfVSCSharpGenerateTypeNoUIEnd = 8530,
                    perfVSCSharpCrossProjectFileParseRequestStart = 8531,
                    perfVSCSharpCrossProjectFileParseRequestEnd = 8532,
                    perfVSCSharpDocumentUpdateRequestStart = 8533,
                    perfVSCSharpDocumentUpdateRequestEnd = 8534,
                    perfVSCSharpFileParseRequestStart = 8535,
                    perfVSCSharpFileParseRequestEnd = 8536,
                    perfVSCSharpMetaDataLoadRequestStart = 8537,
                    perfVSCSharpMetaDataLoadRequestEnd = 8538,
                    perfVSCSharpOnAfterSolutionLoadedRequestStart = 8539,
                    perfVSCSharpOnAfterSolutionLoadedRequestEnd = 8540,
                    perfVSCSharpScanForCommentTaskRequestStart = 8541,
                    perfVSCSharpScanForCommentTaskRequestEnd = 8542,
                    perfVSCSharpSemanticBindRequestStart = 8543,
                    perfVSCSharpSemanticBindRequestEnd = 8544,
                    perfVSCSharpTypeInternalsReclaimationRequestStart = 8545,
                    perfVSCSharpTypeInternalsReclaimationRequestEnd = 8546,
                    perfVSCSharpPrimaryThreadWaitForLockStart = 8547,
                    perfVSCSharpPrimaryThreadWaitForLockEnd = 8548,
                    perfVSCSharpBackgroundThreadWaitForLockStart = 8549,
                    perfVSCSharpBackgroundThreadWaitForLockEnd = 8550,
                    perfVSCSharpOtherThreadWaitForLockStart = 8551,
                    perfVSCSharpOtherThreadWaitForLockEnd = 8552,
                    perfVSCSharpColorizationStart = 8553,
                    perfVSCSharpColorizationEnd = 8554,
                    perfVSCSharpTypeColorizationStart = 8555,
                    perfVSCSharpTypeColorizationEnd = 8556,
                    perfVSCSharpAssemblySetBindNodeStart = 8557,
                    perfVSCSharpAssemblySetBindNodeEnd = 8558,
                    perfVSCSharpIdleExecuteForegroundRequestStart = 8559,
                    perfVSCSharpIdleExecuteForegroundRequestEnd = 8560,
                    perfVSCSharpIdleUpdateEditFilterListStart = 8561,
                    perfVSCSharpIdleUpdateEditFilterListEnd = 8562,
                    perfVSCSharpIdleCleanupSourceDataStringPoolStart = 8563,
                    perfVSCSharpIdleCleanupSourceDataStringPoolEnd = 8564,
                    perfVSCSharpIdleFireCodeModelEventsStart = 8565,
                    perfVSCSharpIdleFireCodeModelEventsEnd = 8566,
                    perfVSCSharpIdleUpdateEditFilterStart = 8567,
                    perfVSCSharpIdleUpdateEditFilterEnd = 8568,
                    perfVSCSharpIdleUpdateCodeDefinitionWindowStart = 8569,
                    perfVSCSharpIdleUpdateCodeDefinitionWindowEnd = 8570,
                    perfVSCSharpIdleHighlightMatchingBracesStart = 8571,
                    perfVSCSharpIdleHighlightMatchingBracesEnd = 8572,
                    perfVSCSharpIdleSynchronizeInstantaneousSmartTagsStart = 8573,
                    perfVSCSharpIdleSynchronizeInstantaneousSmartTagsEnd = 8574,
                    perfVSCSharpIdleSynchronizeDelaySmartTagsStart = 8575,
                    perfVSCSharpIdleSynchronizeDelaySmartTagsEnd = 8576,
                    perfVSCSharpFindAllReferencesStart = 8577,
                    perfVSCSharpFindAllReferencesEnd = 8578,
                    perfVSCSharpReferenceSearchStart = 8579,
                    perfVSCSharpReferenceSearchEnd = 8580,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSDesignerMarkers)
                    

                    
                    
                    perfVSDesignerDragDropHandlerBegin = 8600,
                    
                    perfVSDesignerDragDropHandlerEnd = 8601,
                    perfVSDesignerWinDFGenerator_GenerateOnHostBegin = 8602,
                    perfVSDesignerWinDFGenerator_GenerateOnHostEnd = 8603,
                    perfVSDesignerWinDFGenerator_TableCreatorBegin = 8604,
                    perfVSDesignerWinDFGenerator_TableCreatorEnd = 8605,
                    perfVSDesignerWinDFGenerator_GenerateNavCtrlBegin = 8606,
                    perfVSDesignerWinDFGenerator_GenerateNavCtrlEnd = 8607,
                    
                    perfVSDesignerCiderGenerator_DropHandlerBegin = 8608,
                    
                    perfVSDesignerCiderGenerator_DropHandlerEnd = 8609,

                    
                    perfVSDesigner_DSWin_ShowTreeViewBegin = 8610,
                    
                    perfVSDesigner_DSWin_ShowTreeViewEnd = 8611,

                    
                    perfVSDesigner_DSDesigner_SaveUISettingsBegin = 8620,
                    perfVSDesigner_DSDesigner_SaveUISettingsEnd = 8621,
                    perfVSDesigner_DSDesigner_SaveLayoutBegin = 8622,
                    perfVSDesigner_DSDesigner_SaveLayoutEnd = 8623,
                    perfVSDesigner_DSDesigner_SaveBegin = 8624,
                    perfVSDesigner_DSDesigner_SaveEnd = 8625,


                    
                    perfVSDesigner_AddAppSettingsConnectionsBegin = 8640,
                    perfVSDesigner_AddAppSettingsConnectionsEnd = 8641,
                    perfVSDesigner_DbObjects_ExecuteCommandsBegin = 8642,
                    perfVSDesigner_DbObjects_ExecuteCommandsEnd = 8643,
                    perfVSDesigner_DataSourceBuilder_FillRelationsBegin = 8644,
                    perfVSDesigner_DataSourceBuilder_FillRelationsEnd = 8645,
                    perfVSDesigner_AddConnectionToAppSettingsBegin = 8646,
                    perfVSDesigner_AddConnectionToAppSettingsEnd = 8647,
                    perfVSDesigner_SaveSchemaFileBegin = 8648,
                    perfVSDesigner_SaveSchemaFileEnd = 8649,
                    perfVSDesigner_SetGeneratorPropertyBegin = 8650,
                    perfVSDesigner_SetGeneratorPropertyEnd = 8651,
                    perfVSDesigner_DataSourceGenerator_GenerateCodeBegin = 8652,
                    perfVSDesigner_DataSourceGenerator_GenerateCodeEnd = 8653,

                    
                    perfVSDesigner_DSWizard_FinishBegin = 8670,
                    perfVSDesigner_DSWizard_FinishEnd = 8671,
                    perfVSDesigner_DSWizard_AddXSDFileToProjectBegin = 8672,
                    perfVSDesigner_DSWizard_AddXSDFileToProjectEnd = 8673,

#endif 


#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeMSBuildMarkers)
                    


                    perfMSBuildEngineBuildProjectBegin = 8802,
                    perfMSBuildEngineBuildProjectEnd = 8803,
                    perfMSBuildHostCompileBegin = 8804,
                    perfMSBuildHostCompileEnd = 8805,
                    perfMSBuildProjectLoadFromFileBegin = 8806,
                    perfMSBuildProjectLoadFromFileEnd = 8807,
                    perfMSBuildProjectConstructBegin = 8808,
                    perfMSBuildProjectConstructEnd = 8809,
                    perfMSBuildProjectSaveToFileBegin = 8810,
                    perfMSBuildProjectSaveToFileEnd = 8811,
                    perfMSBuildProjectEvaluateBegin = 8812,
                    perfMSBuildProjectEvaluateEnd = 8813,
                    perfMSBuildGenerateResourceBegin = 8814,
                    perfMSBuildGenerateResourceEnd = 8815,
                    perfMSBuildProjectEvaluatePass0End = 8816,
                    perfMSBuildProjectEvaluatePass1End = 8817,
                    perfMSBuildProjectEvaluatePass2End = 8818,
                    perfMSBuildProjectEvaluatePass3End = 8819,
                    perfMSBuildProjectEvaluatePass4End = 8820,
                    perfMSBuildResolveAssemblyReferenceBegin = 8821,
                    perfMSBuildResolveAssemblyReferenceEnd = 8822,
                    perfMSBuildRARRemoveFromExclusionListBegin = 8823,
                    perfMSBuildRARRemoveFromExclusionListEnd = 8824,
                    perfMSBuildRARComputeClosureBegin = 8825,
                    perfMSBuildRARComputeClosureEnd = 8826,
                    perfMSBuildRARLogResultsBegin = 8827,
                    perfMSBuildRARLogResultsEnd = 8828,
#endif 


#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeWizardFrameworkMarkers)
                    

                    perfWizardFramework_WizardPageReady = 8900,
                    perfTemplateWizardWizardExecuteBegin = 8901,
                    perfTemplateWizardWizardExecuteEnd = 8902,

#endif 


#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSNativeMarkers)
                    
                    
                    
                    
                    perfVSHelpExternalHelpInitializing = 9000,
                    
                    perfVSHelpExternalObjectCreated = 9001,
                    perfVSHelpExternalGotInitData = 9002,
                    perfVSHelpExternalPutHelpOwner = 9003,
                    perfVSHelpExternalGotSettingsManager = 9004,
                    perfVSHelpPutSettingsTokenStart = 9005,
                    perfVSHelpPutSettingsTokenComplete = 9006,
                    perfVSHelpPutSettingsTokenFireSettingsChanged = 9007,
                    perfVSHelpSetCollectionBegin = 9008,
                    perfVSHelpSetCollectionReInitStart = 9009,
                    perfVSHelpSetCollectionReleasedObjects = 9010,
                    perfVSHelpSetCollectionFiredSettingsChanged = 9011,
                    perfVSHelpSetCollectionFiredCollectionChanged = 9012,
                    
                    perfVSHelpExternalHelpInitialized = 9013,
                    perfVSHelpExternalCommunicatedHelpToken = 9014,

                    perfVSHelpServiceF1Begin = 9015,
                    perfVSHelpGotShellContextService = 9016,
                    perfVSHelpGotBrowserWindow = 9017,
                    perfVSHelpGetBrowserWindowFoundModalState = 9018,
                    perfVSHelpGetBrowserWindowGotWBService = 9019,
                    perfVSHelpGetBrowserWindowCreatedWB = 9020,
                    perfVSCreateWebBrowserExStart = 9021,
                    perfVSCreateWebBrowserExNeedCreate = 9022,
                    perfVSCreateWebBrowserExDocDataCreated = 9023,
                    perfVSCreateWebBrowserExPrepareCreateToolWin = 9024,
                    perfVSCreateWebBrowserExCreatedToolWin = 9025,
                    perfVSCreateWebBrowserExToolWinInitialized = 9026,
                    perfVSCreateWebBrowserExDocDataInitialized = 9027,
                    perfVSWebBrowserNavigateComplete2 = 9028,

                    perfHxInitFTSKeywordBegin = 9030,
                    perfHxInitFTSKeywordCreateFTS = 9031,
                    perfHxInitFTSKeywordCreateFTI = 9032,
                    perfHxInitFTSKeywordInitFTI = 9033,
                    perfHxInitFTSInitTitleArray = 9034,
                    perfHxInitFTSInitTitleArrayInitTitlesBegin = 9035,
                    perfHxInitFTSInitTitleArrayInitTitlesEnd = 9036,
                    perfHxInitFTSInitTitleArrayInitFreeHxSEnd = 9037,
                    perfHxInitFTSInitTitleArrayInitHxQEnd = 9038,
                    perfHxInitFTSInitTitleArrayInitHxQ = 9039,
                    perfHxInitFTSInitTitleArrayComputeTopicCount = 9040,
                    perfHxInitFTSInitTitleArrayOutputMapInfo = 9041,
                    perfHxIndexQueryBegin = 9042,
                    perfHxIndexInitializeMergedFileBegin = 9043,
                    perfHxIndexQueryGotOffset = 9044,
                    perfHxIndexQueryFoundMatchingKeyword = 9045,
                    perfHxIndexQueryAddedTopicsForKeyword = 9046,
                    perfHxIndexQueryFoundAllTopics = 9047,
                    perfHxIndexQueryAddedAllTopics = 9048,

                    perfVSHelpIndexPutFilter = 9050,
                    perfVSHelpIndexInitBegin = 9051,
                    perfVSHelpIndexMerged = 9052,
                    perfVSHelpIndexGotXLinkInfo = 9053,
                    perfVSHelpIndexInitWithXLinkInfo = 9054,
                    perfVSHelpMergeIndexBegin = 9055,
                    perfVSHelpMergeIndexDoneCount = 9056,
                    perfVSHelpMergeIndexParsedHxK = 9057,
                    perfVSHelpMergeIndexGotLock = 9058,
                    perfVSHelpMergeIndexInitializedValidator = 9059,
                    perfVSHelpMergeIndexRoundZero = 9060,
                    perfVSHelpMergeIndexRoundOne = 9061,
                    perfVSHelpMergeIndexRoundTwo = 9062,
                    perfVSHelpMergeIndexDoneRounds = 9063,
                    perfVSHelpMergeIndexPersistedHxD = 9064,
                    perfVSHelpMergeIndexComplete = 9065,
                    perfVSHelpInitValidatorBegin = 9066,
                    perfVSHelpInitValidatorGotAccess = 9067,
                    perfVSHelpInitValidatorWithFileBegin = 9068,
                    perfVSHelpInitValidatorOpenedFS = 9069,
                    perfVSHelpInitValidatorCheckedSig = 9070,
                    perfVSHelpInitValidatorOpenedContentTVD = 9071,
                    perfVSHelpInitValidatorOpenedContentFN = 9072,
                    perfVSHelpInitValidatorOpenedMergedTVD = 9073,
                    perfVSHelpInitValidatorOpenedMergedFN = 9074,
                    perfVSHelpInitValidatorLoadedMergedFileData = 9075,
                    perfVSHelpInitValidatorContentsChanged = 9076,
                    perfVSHelpInitValidatorCheckedMergedFileValidity = 9077,

                    perfVSWebBrowserOnDownloadBegin = 9080,
                    perfVSWebBrowserOnDownloadComplete = 9081,
                    perfVSWebBrowserOnBeforeNavigate2 = 9082,
                    perfVSWebBrowserOnProgressChange = 9083,
                    perfVSWebBrowserOnStatusTextChange = 9084,
                    perfVSWebBrowserOnTitleChange = 9085,
                    perfVSWebBrowserOnDocumentComplete = 9086,
                    perfVSWebBrowserOnWindowActivate = 9087,

                    
                    perfVSHelpUserSettingsLoadManagedStart = 9090,
                    
                    perfVSHelpUserSettingsLoadManagedComplete = 9091,
                    perfVSWebPackageLoaded = 9092,
                    perfVSContextServiceStart = 9093,
                    perfVSContextServiceCreated = 9094,
                    perfVSContextServiceLoaded = 9095,
                    perfVSDexploreRun = 9096,
                    perfVSDexploreInitializedPaths = 9097,
                    perfVSDexploreCreatedAppid = 9098,
                    perfVSDexploreDisplayedSplashScreen = 9099,
                    perfVSDexploreStartSetSite = 9100,
                    perfVSDexploreEnsuredAppName = 9101,
                    perfVSDexploreInitializedUserContext = 9102,
                    perfVSDexploreInitAppNameStart = 9103,
                    perfVSDexploreInitAppNameGotBaseName = 9104,
                    perfVSDexploreGotHxSession = 9105,
                    perfVSDexploreGotHxCollection = 9106,
                    perfVSDexploreGotCollectionTitle = 9107,
                    perfVSHelpIndexOnSettingsRootChanged = 9108,
                    perfVSHelpIndexOSRCInitializedUI = 9109,
                    perfVSHelpTocOnSettingsRootChanged = 9110,
                    perfVSHelpTocOSRCInitializedUI = 9111,
                    perfVSHelpXLinkIndexSetFilterBegin = 9112,
                    perfVSHelpXLinkIndexInitialized = 9113,
                    perfVSHelpXLinkIndexFilterSet = 9114,
                    perfVSHelpXLinkIndexInitializeMergedFile = 9115,
                    perfVSHelpXLinkIndexInitializeMergedFileOpenedFS = 9116,
                    perfVSHelpXLinkIndexInitializeMergedFileInitializedLeaves = 9117,
                    perfVSHelpXLinkIndexInitializeMergedFileCachedBranches = 9118,
                    perfVSHelpXLinkIndexInitializeMergedFileCachedPageSlots = 9119,
                    perfVSHelpF1FoundTargetItems = 9120,
                    perfVSHelpF1InitializedDisambiguationData = 9121,
                    perfVSHelpKeywordLookupBegin = 9122,
                    perfVSHelpKeywordLookupGotCWHService = 9123,
                    perfVSHelpKeywordLookupSetIndex = 9124,
                    perfVSHelpKeywordLookupGotTopics = 9125,
                    perfVSHelpKeywordLookupInitializedAttrFilter = 9126,
                    perfVSHelpKeywordLookupLoadedTopicsForKeyword = 9127,
                    perfVSHelpKeywordLookupStartXmlTopics = 9128,
                    perfVSHelpGetTopicsFromKeywordSetIndex = 9129,
                    perfVSHelpGetTopicsFromKeywordGotTopics = 9130,
                    perfVSHelpGetTopicsFromKeywordGotVsTopicList = 9131,
                    perfVSHelpKeywordLookupFoundXmlTopics = 9132,
                    perfVSHelpExternalSetCollection = 9133,
                    perfVSHelpGetTopicsFromKeywordStart = 9134,
                    perfVSContextWindowInitCreateContents = 9135,
                    perfVSContextWindowInitGetMonitorService = 9136,
                    perfVSContextWindowInitGetAppContext = 9137,
                    perfVSContextWindowInitReadRegistry = 9138,
                    perfVSContextWindowInitGetShellContextService = 9139,
                    perfVSContextWindowInitCmdUIContexts = 9140,
                    perfVSContextWindowInitSelection = 9141,
                    perfVSDexploreSetMDIOption = 9142,
                    perfVSDexploreInitCheckLoadPackage = 9143,
                    perfVSHelpInitTocLoadedFilters = 9144,
                    perfVSHelpInitTocCreatedControl = 9145,
                    perfVSHelpInitTocInitControlProperties = 9146,
                    perfVSHelpInitTocStart = 9147,
                    perfVSHelpF1LookupEnsureContextInit = 9148,
                    perfVSHelpF1LookupGotHelp = 9149,
                    perfVSHelpF1LookupGotHelp2 = 9150,
                    perfVSHelpF1LookupGotFirstTimeDlg = 9151,
                    perfVSHelpF1LookupGotContextMonitor = 9152,
                    perfVSHelpF1LookupGotAppCtx = 9153,
                    perfVSHelpF1LookupUpdatedAppCtx = 9154,
                    perfVSHelpF1LookupGotContextAsSafeArray = 9155,
                    perfVSContextWindowCreated = 9156,
                    perfVSLoadContextFilesInitStrings = 9157,
                    perfVSLoadContextFilesFoundFile = 9158,
                    perfVSLoadContextFilesParsedFile = 9159,
                    perfVSDexploreWinMain = 9160,
                    perfVSHelpILocalRegistry = 9161,
                    perfVSDexploreInitParams = 9162,
                    perfVSDexploreInitGuids = 9163,
                    perfVSDexploreInitOle = 9164,
                    perfVSHelpFilterToolInitGotHelpService = 9165,
                    perfVSHelpFilterToolInitGotFilters = 9166,
                    perfVSHelpFilterToolInitAddedFilters = 9167,
                    perfVSHelpFilterToolInitPutFilter = 9168,
                    perfVSHelpFilterToolInitFillComplete = 9169,
                    perfVSHelpFilterToolInitFillBegin = 9170,
                    perfVSHelpHrDoLocalF1LookupBegin = 9171,
                    perfVSHelpHrDoLocalF1LookupContextInit = 9172,
                    perfVSHelpHrDoLocalF1LookupGetContextMonitor = 9173,
                    perfVSHelpHrDoLocalF1LookupUnpackContext = 9174,
                    perfVSHelpHrDoLocalF1LookupGetAppCtx = 9175,
                    perfVSHelpItemLoadKeywordGotTopicAt = 9176,
                    perfVSHelpItemLoadKeywordFilteredTopic = 9177,
                    perfVSHelpGetHelpSettings = 9178,
                    perfVSHelpSettingsInitialized = 9179,
                    perfVSHelpGetF1Preference = 9180,
                    perfVSHelpF1PreferenceFound = 9181,
                    perfVSHelpHrDoLocalF1Begin = 9182,
                    perfVSHelpHrDoOnlineF1Begin = 9183,
                    perfVSHelpHrDoLocalF1Failover = 9184,
                    perfVSHelpOnlineF1Callback = 9185,
                    perfVSHelpGetAllAttrValuesBegin = 9186,
                    perfVSHelpGetAllAttrValuesComplete = 9187,
                    perfVSHelpGetAllAttrValuesGotCollection = 9188,
                    perfVSHelpGetAllAttrValuesGotAttrNames = 9189,
                    perfVSHelpGetAllAttrValuesGotAttrValues = 9190,
                    perfVSHelpF1LookupLoadKeywordBegin = 9191,
                    perfVSHelpF1LookupGetNameBegin = 9192,
                    perfVSHelpF1LookupGetNameComplete = 9193,
                    perfVSHelpF1LookupGetUrlBegin = 9194,
                    perfVSHelpF1LookupGetUrlComplete = 9195,
                    perfVSHelpF1PrepareHeaders = 9196,
                    perfVSHelpF1NavigateWithDisambiguation = 9197,
                    perfVSHelpF1FoundTopicURL = 9198,
                    
                    perfVSInitTestContractsBegin = 9199,
                    
                    perfVSInitTestContractsEnd = 9200,
                    perfVSInitPkgDefLoad = 9201,
                    perfVSCompletePkgDefLoad = 9202,
                    perfVSInitPkgDefRegistryMerge = 9203,
                    perfVSCompletePkgDefRegistryMerge = 9204,
                    perfVSInitPkgDefScan = 9205,
                    perfVSCompletePkgDefScan = 9206,
                    perfVSInitPkgDefRegistryScan = 9207,
                    perfVSCompletePkgDefRegistryScan = 9208,
                    perfVSStartPkgDefInitialization = 9209,
                    perfVSCompletePkgDefInitialization = 9210,

					
					perfVSProjectLoadData = 9211,
					
					perfVSProjectDependencyData = 9212,

                    
                    perfVSThreadedWaitDialogOperationBegin = 9216,
                    
                    perfVSThreadedWaitDialogOperationEnd = 9217,
                    
                    perfVSThreadedWaitDialogShowing = 9218,
                    
                    perfVSThreadedWaitDialogHidden = 9219,

                    perfVSInitFMain = 9220,
                    perfVSInitializedAppIdGlobals = 9221,
                    perfVSInitializedBase = 9222,
                    perfVSInitializedGlobal = 9223,
                    perfVSCheckedTimeBomb = 9224,
                    perfVSSitedAppid = 9225,
                    perfVSCheckedLicensing = 9226,
                    perfVSCheckedActivation = 9227,
                    perfVSInitUIThread = 9228,
                    perfVSInitUIInitializedThemes = 9229,
                    perfVSInitUIInitializedBrushes = 9230,
                    perfVSInitUIInitializedBubble = 9231,
                    perfVSInitUIInitializedMainMenuWin = 9232,
                    perfVSInitUIInitializedDisasterRecovery = 9233,
                    perfVSInitUIModeInitComplete = 9234,
                    perfVSInitUIPreloadedPackages = 9235,
                    perfVSInitUIRegisteredCF = 9236,
                    perfVSInitUIInitializedAppId = 9237,
                    perfVSInitUILoadedInitialProject = 9238,
                    perfVSInitUIOnIDEInitialized = 9239,
                    perfVSInitUICheckedBadAddins = 9240,
                    perfVSInitUIThreadComplete = 9241,
                    perfVSPbrsUpdateFixer = 9242,
                    perfVSMainLoggedPushingMsgLoop = 9243,
                    perfVSCallVsMainFoundMsenv = 9244,
                    perfVSCallVsMainLoadedMsenv = 9245,
                    perfVSCallVsMainFoundVStudioMainProc = 9246,
                    perfVSInitMainMenuWindowInitCommonControls = 9247,
                    perfVSInitMainMenuWindowCreatedHwnd = 9248,
                    perfVSInitMainMenuWindowInitOffice = 9249,
                    perfVSInitMainMenuWindowPrefInitPart2 = 9250,
                    perfVSInitMainMenuWindowCreateVSShellMenu = 9251,
                    perfVSInitMainMenuWindowInitializeCmdbars = 9252,
                    perfVSInitMainMenuWindowSetupCmdUIcontexts = 9253,
                    perfVSInitMainMenuWindowInitDebugMgr = 9254,
                    perfVSInitMainMenuWindowResetContextGuids = 9255,
                    perfVSInitMainMenuWindowInitHierWindow = 9256,
                    perfVSInitMainMenuWindowExtIDEInit = 9257,
                    perfVSInitMainMenuWindowAliases = 9258,
                    perfVSPreloadPackagesLoadedPackage = 9259,
                    perfVSInitMainMenuWindowInitializeMSODialogs = 9260,
                    perfVSLocalRegistryCreateInstanceBegin = 9261,
                    perfVSLocalRegistryVsLoaderCoCreateInstance = 9262,
                    perfVSLocalRegistryGetClassObjectOfClsid = 9263,
                    perfVSLocalRegistryCFCreateInstance = 9264,
                    perfVSLocalRegistryRegisterClassObjectBegin = 9265,
                    perfVSLocalRegistryRegisterClassObjectEnd = 9266,
                    perfVSLocalRegistryRevokeClassObjectBegin = 9267,
                    perfVSLocalRegistryRevokeClassObjectEnd = 9268,
                    perfVSLocalRegistryRegisterInterfaceBegin = 9269,
                    perfVSLocalRegistryRegisterInterfaceEnd = 9270,
                    perfVSLocalRegistryGetClassObjectOfManagedClass = 9271,
                    perfVSLocalRegistryCreateManagedInstanceBegin = 9272,
                    perfVSLocalRegistryCreateManagedInstanceEnd = 9273,
                    perfVSInitGlobalLoadLangDLLMain = 9274,
                    perfVSInitGlobalInitOle = 9275,
                    perfVSInitGlobalInitRegValues = 9276,
                    perfVSInitGlobalCheckDllVersions = 9277,
                    perfVSInitGlobalInitShellFromRegistry = 9278,
                    perfVSInitAutoRegisterInterfaces = 9279,
                    perfVSInitGlobalInitDirectories = 9280,
                    perfVSInitGlobalInitShellFromRegistry2 = 9281,
                    perfVSInitGlobalCreateIdeFonts = 9282,
                    perfVSInitGlobalCreateTheSolution = 9283,
                    perfVSInitGlobalEnsureDDEAtoms = 9284,
                    perfVSInitGlobalSVsAppid = 9285,
                    perfVSInitGlobalRegisterJITDebugger = 9286,
                    perfVSInitGlobalInitMRUs = 9287,
                    perfVSInitGlobalMergeExternalTools = 9288,
                    perfVSInitGlobalVsUIProjWinHierarchyInitClass = 9289,
                    perfVSInitGlobalRestoreFileAssociations = 9290,
                    perfVSMainMenuWindowShown = 9291,
                    perfVSInitMainMenuWindowExtIDEWins = 9292,
                    perfVSInitMainMenuWindowShowDockableWins = 9293,
                    perfVSInitMainMenuWindowFixedPaneWins = 9294,
                    perfVSInitMainMenuWindowResizedAllDocks = 9295,
                    perfVSInitMainMenuWindowComplete = 9296,

                    perfVSPushMessageLoopBegin = 9298,
                    perfVSPushMessageLoopEnd = 9299,

                    
                    perfVSSettingsStartupCheckBegin = 9300,
                    
                    perfVSSettingsStartupCheckComplete = 9301,
                    
                    perfVSSettingsImportStart = 9302,
                    
                    perfVSSettingsImportComplete = 9303,
                    
                    perfVSSettingsExportStart = 9304,
                    
                    perfVSSettingsExportComplete = 9305,
                    
                    perfVSSettingsLoadBegin = 9306,
                    
                    perfVSSettingsLoadComplete = 9307,
                    
                    perfVSSettingsSaveBegin = 9308,
                    
                    perfVSSettingsSaveComplete = 9309,
                    
                    perfVSSettingsCategoryImportStart = 9310,
                    
                    perfVSSettingsCategoryImportComplete = 9311,
                    
                    perfVSSettingsCategoryExportStart = 9312,
                    
                    perfVSSettingsCategoryExportComplete = 9313,

                    perfVSFileChangeAdviseFileChangeBegin = 9320,
                    perfVSFileChangeAdviseFileChangeEnd = 9321,
                    perfVSFileChangeUnadviseFileChangeBegin = 9322,
                    perfVSFileChangeUnadviseFileChangeEnd = 9323,
                    perfVSFileChangeSyncFileBegin = 9324,
                    perfVSFileChangeSyncFileEnd = 9325,
                    perfVSFileChangeIgnoreFileBegin = 9326,
                    perfVSFileChangeIgnoreFileEnd = 9327,
                    perfVSFileChangeIgnoreAdviseBegin = 9328,
                    perfVSFileChangeIgnoreAdviseEnd = 9329,
                    perfVSFileChangeAdviseDirChangeBegin = 9330,
                    perfVSFileChangeAdviseDirChangeEnd = 9331,
                    perfVSFileChangeUnadviseDirChangeBegin = 9332,
                    perfVSFileChangeUnadviseDirChangeEnd = 9333,
                    perfVSFileChangeIgnoreDirBegin = 9334,
                    perfVSFileChangeIgnoreDirEnd = 9335,
                    perfVSFileChangeDirChangedBegin = 9336,
                    perfVSFileChangeDirChangedEnd = 9337,
                    perfVSFileChangeBrakeOn = 9338,
                    perfVSFileChangeBrakeOff = 9339,

                    perfVSSolutionUpdateRunBuildEnter = 9340,
                    perfVSSolutionUpdateRunBuildInnerLoop = 9341,
                    perfVSSolutionUpdateRunBuildExit = 9342,
                    perfVSSolutionUpdateEndSlnUpdateEnter = 9343,
                    perfVSSolutionUpdateEndSlnUpdateExit = 9344,

                    perfVSInitDelayLoadOfUIDLLsBegin = 9350,
                    perfVSInitDelayLoadOfUIDLLsEndIteration = 9351,
                    perfVSInitDelayLoadOfUIDLLsEnd = 9352,

					perfVSStubDocumentCreated = 9353,
					perfVSStubDocumentInitializing = 9354,
					perfVSStubDocumentInitialized = 9355,

                    perfHxProtocolInitBegin = 9400,
                    perfHxProtocolInitComplete = 9401,
                    perfHxProtocolInternalStartBegin = 9402,
                    perfHxProtocolInternalStartGotPhysicalUrl = 9403,
                    perfHxProtocolInternalStartItsProtocolInitialized = 9404,
                    perfHxProtocolInternalItsStartComplete = 9405,
                    perfHxProtocolInternalIECacheCleared = 9406,
                    perfHxIndexTopicId2TopicArrayBegin = 9407,
                    perfHxTitleInformationInitializeBegin = 9408,
                    perfHxTitleInformationInitializeOpenedTxt = 9409,
                    perfHxTitleInformationInitializeVersionCorrect = 9410,
                    perfHxTitleInformationInitializeComplete = 9411,
                    perfHxTitleGetTopicURLBegin = 9412,
                    perfHxTitleGotTopicData = 9413,
                    perfHxTitleGotUrlOffset = 9414,
                    perfHxInitTitleBegin = 9415,
                    perfHxInitTitleOpenedSubFiles = 9416,
                    perfHxInitTitleComplete = 9417,

					
					perfVSOpenDocumentInfo = 9420,

                    perfVSWebPkgCreated = 9430,
                    perfVSWebPkgSetSiteBegin = 9431,
                    perfVSWebPkgSetModuleSite = 9432,
                    perfVSWebPkgSetSiteUILibraryLoaded = 9433,
                    perfVSWebPkgSetSitePrefsLoaded = 9434,
                    perfVSWebPkgSetSiteServicesProffered1 = 9435,
                    perfVSWebPkgSetSiteContextTrackerCreated = 9436,
                    perfVSWebPkgSetSiteServicesProffered2 = 9437,

                    perfVSHeapCreateBegin = 9438,
                    perfVSHeapCreateComplete = 9439,

                    perfVSSlnBuildBegin = 9440,
                    perfVSSlnBuildEnd = 9441,

                    perfVSIdleBegin = 9442,
                    perfVSIdleEnd = 9443,
                    perfVSIdleDelay = 9444,
                    perfVSInputDelay = 9445,
                    perfVSPerfWatsonUnResponsive = 9446,
                    perfVSPerfWatsonUnResponsiveBegin = 9447,
                    perfVSPerfWatsonUnResponsiveEnd = 9448,
                    perfVSMenuOpenBegin = 9449,
                    perfVSMenuOpenEnd = 9450,
                    perfVSIdleSkipped = 9451,

                    perfVSDesignTimeBuildBegin = 9452,
                    perfVSDesignTimeBuildEnd = 9453,
                    perfVSWriteUserOptsFileBegin = 9454,
                    perfVSWriteUserOptsFileEnd = 9455,

                    
                    perfVSOpenDocumentBegin = 9456,
                    
                    perfVSOpenDocumentEnd = 9457,

                    perfVSPreviewChangesDialogInitialized = 9458,

                    
                    perfVSSlnConfigurationChangeBegin = 9459,
                    
                    perfVSSlnConfigurationChangeEnd = 9460,

                    
                    perfVSDTBuildsComplete = 9461,

                    
                    perfVSTaskListAsyncRefreshTasksEnd = 9462,

                    
                    perfVSTaskListAsyncRefreshTasksUpdateTaskListStateBegin = 9463,

                    
                    perfVSTaskListAsyncRefreshTasksUpdateTaskListStateEnd = 9464,

                    
                    perfVSTaskListAsyncRefreshTasksUpdateUIBegin = 9465,

                    
                    perfVSTaskListAsyncRefreshTasksUpdateUIEnd = 9466,

                    
                    perfVSTaskListAsyncRefreshTasksInvalidateProviderItemsBegin = 9467,

                    
                    perfVSTaskListAsyncRefreshTasksInvalidateProviderItemsEnd = 9468,

                    
                    perfVSTaskListAsyncRefreshTasksInvalidateProviderHiddenItemsBegin = 9469,

                    
                    perfVSTaskListAsyncRefreshTasksInvalidateProviderHiddenItemsEnd = 9470,

                    
                    perfVSTaskListAsyncRefreshTasksEnumerateProviderItemsBegin = 9471,

                    
                    perfVSTaskListAsyncRefreshTasksEnumerateProviderItemsEnd = 9472,

                    
                    perfVSTaskListAsyncRefreshTasksProcessProviderItemsBegin = 9473,

                    
                    perfVSTaskListAsyncRefreshTasksProcessProviderItemsEnd = 9474,

                    
                    perfVSTaskListAsyncRefreshTasksProcessProviderItemsRanOutOfTime = 9475,

                    
                    perfVSTaskListAsyncRefreshTasksEliminateInvalidProviderItemsBegin = 9476,

                    
                    perfVSTaskListAsyncRefreshTasksEliminateInvalidProviderItemsEnd = 9477,

                    
                    perfVSTaskListAsyncRefreshTasksEliminateHiddenInvalidProviderItemsBegin = 9478,

                    
                    perfVSTaskListAsyncRefreshTasksEliminateHiddenInvalidProviderItemsEnd = 9479,

                    
                    perfVSTaskListAsyncRefreshTasksCompactChangedProvidersBegin = 9480,

                    
                    perfVSTaskListAsyncRefreshTasksCompactChangedProvidersEnd = 9481,

                    
                    perfVSTaskListAsyncRefreshTasksSortProviderItemsBegin = 9482,

                    
                    perfVSTaskListAsyncRefreshTasksSortProviderItemsEnd = 9483,

                    
                    perfVSTaskListAsyncRefreshTasksUpdateProviderHiddenItemInfoBegin = 9484,

                    
                    perfVSTaskListAsyncRefreshTasksUpdateProviderHiddenItemInfoEnd = 9485,

                    
                    perfVSTaskListAsyncRefreshTasksProviderUIBegin = 9486,

                    
                    perfVSTaskListAsyncRefreshTasksProviderUIEnd = 9487,

                    
                    perfVSTaskListAsyncRefreshOrAddTasksBegin = 9488,

                    
                    perfVSTaskListAsyncRefreshOrAddTasksEnd = 9489,

                    
                    perfVSTaskListAsyncRemoveItemsBegin = 9490,

                    
                    perfVSTaskListAsyncRemoveItemsEnd = 9491,

                    
                    perfVSTaskListAsyncRefreshAllProvidersBegin = 9492,

                    
                    perfVSTaskListAsyncRefreshAllProvidersEnd = 9493,

                    
                    perfVSTaskListAsyncRefreshTasksBegin = 9494,

                    
                    perfVSCompactUserOptsFileBegin = 9495,

                    
                    perfVSCompactUserOptsFileEnd = 9496,

                    perfVSFileChangeOnBeforeCloseSolutionBegin = 9497,
                    perfVSFileChangeOnBeforeCloseSolutionEnd = 9498,

                    
                    perfVSTaskListSyncRefreshTasksBegin = 9499,

#endif 


#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSTFMarkers)
                    
                    

                    
                    perfVSTF_WITUIConnectBegin = 9500,
                    perfVSTF_WITUIConnectEnd = 9501,
                    perfVSTF_WITUIQueryBegin = 9502,
                    perfVSTF_WITUIQueryEnd = 9503,
                    perfVSTF_WITUIQueryCountBegin = 9504,
                    perfVSTF_WITUIQueryCountEnd = 9505,
                    perfVSTF_WITUIWorkItemNewBegin = 9506,
                    perfVSTF_WITUIWorkItemNewEnd = 9507,
                    perfVSTF_WITUIWorkItemOpenBegin = 9508,
                    perfVSTF_WITUIWorkItemOpenEnd = 9509,
                    perfVSTF_WITUIWorkItemSaveBegin = 9510,
                    perfVSTF_WITUIWorkItemSaveEnd = 9511,
                    perfVSTF_WITUIWorkItemCopyBegin = 9512,
                    perfVSTF_WITUIWorkItemCopyEnd = 9513,
                    perfVSTF_WITUIStoredQuerySaveBegin = 9514,
                    perfVSTF_WITUIStoredQuerySaveEnd = 9515,
                    perfVSTF_WITUIAttachmentUploadBegin = 9516,
                    perfVSTF_WITUIAttachmentUploadEnd = 9517,
                    perfVSTF_WITUIDisplayItemBegin = 9518,
                    perfVSTF_WITUIDisplayItemEnd = 9519,
                    perfVSTF_WITUISaveDataBegin = 9520,
                    perfVSTF_WITUISaveDataEnd = 9521,
                    perfVSTF_WITUITriageViewDisplayBegin = 9522,
                    perfVSTF_WITUITriageViewDisplayEnd = 9523,
                    perfVSTF_WITUITriageQueryRefreshBegin = 9524,
                    perfVSTF_WITUITriageQueryRefreshEnd = 9525,

                    perfVSTF_WITOMConnectBegin = 9526,
                    perfVSTF_WITOMConnectEnd = 9527,
                    perfVSTF_WITOMQueryBegin = 9528,
                    perfVSTF_WITOMQueryEnd = 9529,
                    perfVSTF_WITOMWorkItemOpenBegin = 9530,
                    perfVSTF_WITOMWorkItemOpenEnd = 9531,
                    perfVSTF_WITOMWorkItemSaveBegin = 9532,
                    perfVSTF_WITOMWorkItemSaveEnd = 9533,

                    perfVSTF_WITProxyConnectBegin = 9534,
                    perfVSTF_WITProxyConnectEnd = 9535,
                    perfVSTF_WITProxyQueryBegin = 9536,
                    perfVSTF_WITProxyQueryEnd = 9537,
                    perfVSTF_WITProxyGetWorkItemBegin = 9538,
                    perfVSTF_WITProxyGetWorkItemEnd = 9539,
                    perfVSTF_WITProxyUpdateBegin = 9540,
                    perfVSTF_WITProxyUpdateEnd = 9541,

                    perfVSTF_WITOMWorkItemPartialOpenBegin = 9542,
                    perfVSTF_WITOMWorkItemPartialOpenEnd = 9543,
                    perfVSTF_WITOMBatchSaveBegin = 9544,
                    perfVSTF_WITOMBatchSaveEnd = 9545,

                    perfVSTF_WITProxyBulkSaveBegin = 9546,
                    perfVSTF_WITProxyBulkSaveEnd = 9547,

                    
                    
                    perfVSTR_WITUIResultViewLastPageBegin = 9548,
                    perfVSTR_WITUIResultViewLastPageEnd = 9549,
                    perfVSTR_WITUIViewQueryBegin = 9550,
                    perfVSTR_WITUIViewQueryEnd = 9551,
                    perfVSTR_WITUIGetListOfGroupsBegin = 9552,
                    perfVSTR_WITUIUIGetListOfGroupsEnd = 9553,
                    perfVSTF_WITProxyPageItemsBegin = 9554,
                    perfVSTF_WITProxyPageItemsEnd = 9555,
                    perfVSTF_WITProxyPageItemsByPairsBegin = 9556,
                    perfVSTF_WITProxyPageItemsByPairsEnd = 9557,

                    
                    perfVSTF_WITUIShowNodeBegin = 9558,
                    
                    perfVSTF_WITUIShowNodeEnd = 9559,
                    perfVSTF_WITUIQueryEditorRefreshBegin = 9560,
                    perfVSTF_WITUIQueryEditorRefreshEnd = 9561,
                    
                    perfVSTF_WITShellDirtyChanged = 9562,


                    
                    
                    perfVSTF_WebServiceBegin = 9600,
                    
                    perfVSTF_WebServiceEnd = 9601,

                    
                    perfVSTF_ELeadExcelCreateReportBegin = 9650,
                    perfVSTF_ELeadExcelCreateReportEnd = 9651,

                    perfVSTF_ReportingQueryChooserVisible = 9658,
                    perfVSTF_ReportingProgressTrackerVisible = 9659,
                    perfVSTF_ReportingTranslationStatusVisible = 9660,

                    perfVSTF_ReportingWizardOnTaskCancelled = 9661,
                    perfVSTF_ReportingWizardOnTaskComplete = 9662,
                    perfVSTF_ReportingWizardShutDown = 9663,

                    perfVSTF_ReportingGenerateReportBegin = 9664,
                    perfVSTF_ReportingGenerateReportEnd = 9665,
                    perfVSTF_ReportingWiqlParseBegin = 9666,
                    perfVSTF_ReportingWiqlParseEnd = 9667,
                    perfVSTF_ReportingCreatePivotTableBegin = 9668,
                    perfVSTF_ReportingCreatePivotTableEnd = 9669,

                    perfVSTF_ReportingOleDbExecuteCellSetBegin = 9670,
                    perfVSTF_ReportingOleDbExecuteCellSetEnd = 9671,

                    
                    perfVSTF_ReportingShowNodeBegin = 9672,
                    
                    perfVSTF_ReportingShowNodeEnd = 9673,

                    
                    perfVSTF_TeamBuildUIWorkBegin = 9700,
                    perfVSTF_TeamBuildUIWorkEnd = 9701,
                    perfVSTF_TeamBuildShowBuildTypesNodeBegin = 9702,
                    perfVSTF_TeamBuildShowBuildTypesNodeEnd = 9703,
                    perfVSTF_TeamBuildGetBuildTypeInfoBegin = 9704,
                    perfVSTF_TeamBuildGetBuildTypeInfoEnd = 9705,
                    perfVSTF_TeamBuildStartBuildBegin = 9706,
                    perfVSTF_TeamBuildStartBuildEnd = 9707,
                    perfVSTF_TeamBuildShowSummaryPageBegin = 9708,
                    perfVSTF_TeamBuildShowSummaryPageEnd = 9709,
                    perfVSTF_TeamBuildRefreshSummaryPageBegin = 9710,
                    perfVSTF_TeamBuildRefreshSummaryPageEnd = 9711,
                    perfVSTF_TeamBuildDeleteBuildBegin = 9712,
                    perfVSTF_TeamBuildDeleteBuildEnd = 9713,
                    perfVSTF_TeamBuildCheckoutTeamBuildProjectFileBegin = 9714,
                    perfVSTF_TeamBuildCheckoutTeamBuildProjectFileEnd = 9715,
                    perfVSTF_TeamBuildShowDetailedReportBegin = 9716,
                    perfVSTF_TeamBuildShowDetailedReportEnd = 9717,
                    perfVSTF_TeamBuildShowDetailedReportSummaryBegin = 9718,
                    perfVSTF_TeamBuildShowDetailedReportSummaryEnd = 9719,
                    perfVSTF_TeamBuildShowDetailedReportBuildStepsBegin = 9720,
                    perfVSTF_TeamBuildShowDetailedReportBuildStepsEnd = 9721,
                    perfVSTF_TeamBuildShowDetailedReportFlavorResultsBegin = 9722,
                    perfVSTF_TeamBuildShowDetailedReportFlavorResultsEnd = 9723,
                    perfVSTF_TeamBuildShowDetailedReportChangesetsBegin = 9724,
                    perfVSTF_TeamBuildShowDetailedReportChangesetsEnd = 9725,
                    perfVSTF_TeamBuildShowDetailedReportWorkItemsBegin = 9726,
                    perfVSTF_TeamBuildShowDetailedReportWorkItemsEnd = 9727,
                    perfVSTF_TeamBuildReportBuildProgressStart = 9728,
                    perfVSTF_TeamBuildReportBuildProgressEnd = 9729,
                    perfVSTF_TeamBuildGetReportDataBegin = 9730,
                    perfVSTF_TeamBuildGetReportDataEnd = 9731,
                    perfVSTF_TeamBuildUpdateReportUiBegin = 9732,
                    perfVSTF_TeamBuildUpdateReportUiEnd = 9733,
                    perfVSTF_TeamBuildBuildProcessBegin = 9734,
                    perfVSTF_TeamBuildBuildProcessEnd = 9735,
                    perfVSTF_TeamBuildSwitchBuildExplorerTabBegin = 9736,
                    perfVSTF_TeamBuildSwitchBuildExplorerTabEnd = 9737,


                    
                    perfEDT_TestViewIdleProcessingBegin = 9750,
                    perfEDT_TestViewIdleProcessingEnd = 9751,
                    perfEDT_TestManagerAfterTestsLoaded = 9752,
                    perfEDT_TestResultsTestRunStarted = 9753,
                    perfEDT_TestResultsTestRunFinished = 9754,
                    perfEDT_UnitTestGenerationBegin = 9755,
                    perfEDT_UnitTestGenerationEnd = 9756,
                    perfEDT_TestResultsTestRunLoaded = 9757,
                    perfEDT_TestResultsTestRunDeleted = 9758,
                    perfEDT_GroupingTreeItemToggled = 9759,
                    perfEDT_TestManagerTestDisabled = 9760,
                    perfEDT_DetailedTestResultsRendered = 9761,
                    perfEDT_CodeCoverageExpandStatistics = 9762,
                    perfEDT_CodeCoverageNavigate = 9763,
                    perfEDT_TmiLoadResultsFinished = 9764,
                    perfEDT_TmiSaveRunResultsFinished = 9765,
                    perfEDT_IdleProcessManagerWorkCompleted = 9766,
                    perfEDT_CmiElementAdded = 9767,
                    perfEDT_CmiElementChanged = 9768,
                    perfEDT_CmiElementDeleted = 9769,
                    perfEDT_PublishResultCopyStarted = 9770,
                    perfEDT_PublishResultCopyFinished = 9771,
                    perfEDT_PublishResultDatabaseStarted = 9772,
                    perfEDT_PublishResultDatabaseFinished = 9773,

                    perfEDT_IDEAnalysisRenderBegin = 9780,
                    perfEDT_IDEAnalysisRenderEnd = 9781,

                    perfEDT_LoadTestRunManagementOpenCompleted = 9790,
                    perfEDT_LoadTestRunManagementRemoveCompleted = 9791,
                    perfEDT_LoadTestRunManagementExportCompleted = 9792,
                    perfEDT_LoadTestRunManagementImportCompleted = 9793,
                    perfEDT_LoadTestRunManagementRunsLoaded = 9794,

                    
                    
                    perfVSTF_ELeadRefreshTeamExplorerAsyncBegin = 9800,
                    
                    perfVSTF_ELeadRefreshTeamExplorerAsyncEnd = 9801,
                    
                    perfVSTF_ELeadConnectToTfsDlgUpdateProjectListEnd = 9802,
                    
                    perfVSTF_ELeadProjectCreationBegin = 9803,
                    
                    perfVSTF_ELeadProjectCreationEndSuccess = 9804,
                    
                    perfVSTF_ELeadProjectCreationEndFailure = 9805,
                    
                    perfVSTF_ELeadExcelCreateNewListEnd = 9806,
                    
                    perfVSTF_ELeadExcelCreateNewListBegin = 9807,
                    
                    perfVSTF_ELeadExcelAddWorkitemsBegin = 9808,
                    
                    perfVSTF_ELeadExcelAddWorkitemsEnd = 9809,
                    
                    perfVSTF_ELeadWIPFindWorkitemsBegin = 9810,
                    
                    perfVSTF_ELeadWIPFindWorkitemsEnd = 9811,
                    
                    perfVSTF_ELeadExcelRefreshPublishBegin = 9812,
                    
                    perfVSTF_ELeadExcelRefreshPublishEnd = 9813,
                    
                    perfVSTF_ELeadExcelUpdateColumnsBegin = 9814,
                    
                    perfVSTF_ELeadExcelUpdateColumnsEnd = 9815,
                    
                    perfVSTF_ELeadTeamExplorerExpandPluginBegin = 9816,
                    
                    perfVSTF_ELeadTeamExplorerExpandPluginEnd = 9817,
                    
                    perfVSTF_ELeadMSProjectRefreshPublishBegin = 9818,
                    
                    perfVSTF_ELeadMSProjectRefreshPublishEnd = 9819,
                    
                    perfVSTF_ELeadMSProjectAddWorkitemsBegin = 9820,
                    
                    perfVSTF_ELeadMSProjectAddWorkitemsEnd = 9821,
                    
                    perfVSTF_ELeadGroupsPropertiesOKClickBegin = 9822,
                    
                    perfVSTF_ELeadGroupsPropertiesOKClickEnd = 9823,
                    
                    perfVSTF_ELeadGroupsViewMemberPropertiesEnd = 9824,
                    
                    perfVSTF_ELeadGroupsDeleteGroupBegin = 9825,
                    
                    perfVSTF_ELeadGroupsDeleteGroupEnd = 9826,
                    
                    perfVSTF_ELeadGroupsCreateNewGroupBegin = 9827,
                    
                    perfVSTF_ELeadGroupsCreateNewGroupEnd = 9828,
                    
                    perfVSTF_ELeadTemplateUploadingBegin = 9829,
                    
                    perfVSTF_ELeadTemplateUploadingEnd = 9830,
                    
                    perfVSTF_ELeadTemplateDownloadingBegin = 9831,
                    
                    perfVSTF_ELeadTemplateDownloadingEnd = 9832,
                    
                    perfVSTF_ELeadExcelCreateDocumentBegin = 9833,
                    
                    perfVSTF_ELeadExcelCreateDocumentEnd = 9834,
                    
                    perfVSTF_ELeadProjectCreateDocumentBegin = 9835,
                    
                    perfVSTF_ELeadProjectCreateDocumentEnd = 9836,
                    
                    perfVSTF_ELeadViewClassificationsBegin = 9837,
                    
                    perfVSTF_ELeadViewClassificationsEnd = 9838,
                    
                    perfVSTF_ELeadAddTFSBegin = 9839,
                    
                    perfVSTF_ELeadAddTFSEnd = 9840,
                    
                    perfVSTF_ELeadChangeAlertBegin = 9841,
                    
                    perfVSTF_ELeadChangeAlertEnd = 9842,
                    
                    perfVSTF_ELeadShowSecurityBegin = 9843,
                    
                    perfVSTF_ELeadShowSecurityEnd = 9844,
                    
                    perfVSTF_ELeadShowGroupsBegin = 9845,
                    
                    perfVSTF_ELeadShowGroupsEnd = 9846,
                    
                    perfVSTF_ELeadExcelAddSubLevelBegin = 9847,
                    
                    perfVSTF_ELeadExcelAddSubLevelEnd = 9848,
                    
                    perfVSTF_ELeadRefreshTeamExplorerReady = 9849,

                    
                    
                    perfVSTF_SCCExplorerUpdateBegin = 9850,
                    
                    perfVSTF_SCCExplorerUpdateEnd = 9851,
                    
                    perfVSTF_SCCSettingBegin = 9852,
                    
                    perfVSTF_SCCSettingEnd = 9853,
                    perfVSTF_SCCApplyLabelBegin = 9854,
                    perfVSTF_SCCApplyLabelEnd = 9855,
                    perfVSTF_SCCUndeleteBegin = 9856,
                    perfVSTF_SCCUndeleteEnd = 9857,
                    
                    perfVSTF_SCCPopulateHistoryDlgBegin = 9858,
                    
                    perfVSTF_SCCPopulateHistoryDlgEnd = 9859,
                    perfVSTF_SCCPopulateChangeListDlgBegin = 9860,
                    perfVSTF_SCCPopulateChangeListDlgEnd = 9861,
                    perfVSTF_SCCBranchBegin = 9862,
                    perfVSTF_SCCBranchEnd = 9863,
                    perfVSTF_SCCDeleteBegin = 9864,
                    perfVSTF_SCCDeleteEnd = 9865,
                    perfVSTF_SCCRenameBegin = 9866,
                    perfVSTF_SCCRenameEnd = 9867,
                    perfVSTF_SCCMergeBegin = 9868,
                    perfVSTF_SCCMergeEnd = 9869,
                    
                    perfVSTF_SCERefreshBegin = 9870,
                    
                    perfVSTF_SCERefreshEnd = 9871,
                    
                    perfVSTF_FolderDiffCompareBegin = 9872,
                    
                    perfVSTF_FolderDiffCompareEnd = 9873,
                    
                    perfVSTF_FolderDiffComparePath1Queried = 9874,
                    
                    perfVSTF_FolderDiffComparePath2Queried = 9875,
                    
                    perfVSTF_FolderDiffComparePendingChangesQueried = 9876,
                    
                    perfVSTF_FolderDiffCompareDifferencesCalculated = 9877,
                    
                    perfVSTF_SCCPackageBegin = 9878,
                    
                    perfVSTF_SCEAsyncRefillBegin = 9879,
                    
                    perfVSTF_SCESyncRefillBegin = 9880,
                    
                    perfVSTF_SCESyncRefillEnd = 9881,
                    
                    perfVSTF_VCConflictRefreshBegin = 9882,
                    
                    perfVSTF_VCConflictRefreshEnd = 9883,
                    
                    perfVSTF_VCConflictResolveBegin = 9884,
                    
                    perfVSTF_VCConflictResolveEnd = 9885,
                    
                    perfVSTF_SCCPopulateHistoryLabelsBegin = 9886,
                    
                    perfVSTF_SCCPopulateHistoryLabelsEnd = 9887,
                    
                    perfVSTF_BVQueryAllBranchesBegin = 9888,
                    
                    perfVSTF_BVQueryAllBranchesEnd = 9889,
                    
                    perfVSTF_BVQuerySourceBranchesBegin = 9890,
                    
                    perfVSTF_BVQuerySourceBranchesEnd = 9891,
                    
                    perfVSTF_BVTrackingMergesBegin = 9892,
                    
                    perfVSTF_BVTrackingMergesEnd = 9893,
                    
                    perfVSTF_LabelCreateBegin = 9894,
                    
                    perfVSTF_LabelCreateEnd = 9895,

                    

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeTSDataMarkers)

                    
                   perfTsData_SchemaCompareBegin = 9900,
                    perfTsData_SchemaCompareCanceled = 9901,
                    perfTsData_SchemaCompareEnd = 9902,
                    perfTsData_DataCompareBegin = 9903,
                    perfTsData_DataCompareCanceled = 9904,
                    perfTsData_DataCompareEnd = 9905,
                    perfTsData_SqlEditorExecuteBegin = 9906,
                    perfTsData_SqlEditorExecuteEnd = 9907,
                    perfTsData_ProjectLoadBegin = 9920,
                    perfTsData_ProjectLoadEnd = 9921,
                    perfTsData_ProjectWizardImportSchemaFinishBegin = 9922,
                    perfTsData_ProjectWizardImportSchemaFinishEnd = 9923,
                    perfTsData_ProjectBuildBegin = 9924,
                    perfTsData_ProjectBuildEnd = 9925,
                    perfTsData_ProjectDeployBegin = 9928,
                    perfTsData_ProjectDeployEnd = 9929,
                    perfTsData_DataGenerateBegin = 9950,
                    perfTsData_DataGenerateEnd = 9951,
                    perfTsData_DataGeneratorDeserializeBegin = 9952,
                    perfTsData_DataGeneratorDeserializeEnd = 9953,
                    perfTsData_DataGeneratorRetrieveSchemaBegin = 9954,
                    perfTsData_DataGeneratorRetrieveSchemaEnd = 9955,
                    perfTsData_DisplayAdapterSchemaObjectChangeDone = 9956,
                    perfTsData_SchemaViewNodePopulationComplete = 9957,
                    perfTsData_ConnectionStringPersistedInRegistry = 9958,
                    perfTsData_SchemaManagerRecycleBegin = 9959,
                    perfTsData_SchemaManagerRecycleEnd = 9960,
                    perfTsData_ReverseEngineerBegin = 9961,
                    perfTsData_ReverseEngineerEnd = 9962,
                    perfTsData_ImportScriptBegin = 9963,
                    perfTsData_ImportScriptEnd = 9964,

                    perfTsData_DataCompare_ScriptExportBegin = 9965,
                    perfTsData_DataCompare_ScriptExportEnd = 9966,
                    perfTsData_DataCompare_WriteUpdatesBegin = 9967,
                    perfTsData_DataCompare_WriteUpdatesEnd = 9968,
                    perfTsData_DataCompare_RefreshScriptBegin = 9969,
                    perfTsData_DataCompare_RefreshScriptEnd = 9970,
                    perfTsData_DataCompare_DataPopulationBegin = 9971,
                    perfTsData_DataCompare_DataPopulationEnd = 9972,
                    perfTsData_DataCompare_FilterBegin = 9973,
                    perfTsData_DataCompare_FilterEnd = 9974,

#endif 

                    

                    

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeCiderMarkers)
                    
                    perfCiderFirstMarker = 16000,
                    perfCiderOpenXAMLBegin = 16001,
                    perfCiderOpenXAMLEnd = 16002,
                    perfCiderDispatchIdleEvent = 16003,
                    perfCiderSwitchToDesignViewBegin = 16004,
                    perfCiderSwitchToXAMLViewBegin = 16005,
                    perfCiderCreateControlBegin = 16006,
                    perfCiderCreateControlEnd = 16007,
                    perfCiderSelectionChangeBegin = 16008,
                    perfCiderSelectionChangeEnd = 16009,
                    perfCiderSelectAllBegin = 16010,
                    perfCiderSelectAllEnd = 16011,
                    perfCiderReparentBegin = 16012,
                    perfCiderReparentEnd = 16013,
                    perfCiderCutBegin = 16014,
                    perfCiderCutEnd = 16015,
                    perfCiderCopyBegin = 16016,
                    perfCiderCopyEnd = 16017,
                    perfCiderPasteBegin = 16018,
                    perfCiderPasteEnd = 16019,
                    perfCiderPropertyChangeBegin = 16020,
                    perfCiderPropertyChangeEnd = 16021,
                    perfCiderRenameRefactorBegin = 16022,
                    perfCiderRenameRefactorEnd = 16023,
                    perfCiderBuildBegin = 16024,
                    perfCiderBuildEnd = 16025,
                    perfCiderListMembersBegin = 16026,
                    perfCiderListMembersEnd = 16027,
                    perfCiderStatementCompletionBegin = 16028,
                    perfCiderStatementCompletionEnd = 16029,
                    perfCiderSwitchToDesignViewEnd = 16030,
                    perfCiderSwitchToXAMLViewEnd = 16031,
                    perfCiderSaveXAMLBegin = 16032,
                    perfCiderSaveXAMLEnd = 16033,
                    perfCiderDesignerReloadBegin = 16034,
                    perfCiderDesignerReloadEnd = 16035,
                    perfCiderAppDomainReloadBegin = 16036,
                    perfCiderAppDomainReloadEnd = 16037,
                    perfCiderSelectNextBegin = 16038,
                    perfCiderSelectNextEnd = 16039,
                    perfCiderSelectPreviousBegin = 16040,
                    perfCiderSelectPreviousEnd = 16041,
                    perfCiderModelPropertyValueGetBegin = 16042,
                    perfCiderModelPropertyValueGetEnd = 16043,
                    perfCiderPropertyInspectorSelectionChangeBegin = 16044,
                    perfCiderPropertyInspectorSelectionChangeEnd = 16045,
                    perfCiderPropertyInspectorInitializeBegin = 16046,
                    perfCiderPropertyInspectorInitializeEnd = 16047,
                    perfCiderMarqueeSelectBegin = 16048,
                    perfCiderMarqueeSelectEnd = 16049,
                    perfCiderResizeStart = 16050,
                    perfCiderResizeEnd = 16051,
                    perfCiderActivateViewBegin = 16052,
                    perfCiderActivateViewEnd = 16053,
                    perfCiderAutoToolboxUpdateBegin = 16054,
                    perfCiderAutoToolboxUpdateEnd = 16055,
                    perfCiderAdornerMeasureOverrideBegin = 16056,
                    perfCiderAdornerMeasureOverrideEnd = 16057,
                    perfCiderAdornerArrangeOverrideBegin = 16058,
                    perfCiderAdornerArrangeOverrideEnd = 16059,
                    perfCiderPropertyMarkerConvertBegin = 16060,
                    perfCiderPropertyMarkerConvertEnd = 16061,
                    perfCiderPropertyInspectorPopupOpen = 16062,
                    perfCiderPropertyInspectorPopupClose = 16063,
                    perfCiderAppDomainUnloadBegin = 16064,
                    perfCiderAppDomainUnloadEnd = 16065,
                    perfCiderLastMarker = 16199,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeSmartDevicesMarkers)
                    
                    
                    perfVSDSIDEGetConfigBegin = 16200,
                    
                    perfVSDSIDEGetConfigEnd = 16201,
                    
                    perfVSDSIDESetConfigBegin = 16202,
                    
                    perfVSDSIDESetConfigEnd = 16203,
                    
                    perfVSDSIDEGetCertBegin = 16204,
                    
                    perfVSDSIDEGetCertEnd = 16205,
                    
                    perfVSDSIDEAddCertBegin = 16206,
                    
                    perfVSDSIDEAddCertEnd = 16207,
                    
                    perfVSDSIDEDeleteCertBegin = 16208,
                    
                    perfVSDSIDEDeleteCertEnd = 16209,
                    
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeEDMToolsMarkers)
                    
                    perfEDMTools_WizardRunStartedBegin = 16300,
                    perfEDMTools_WizardRunStartedEnd = 16301,
                    perfEDMTools_WizardRunFinishedBegin = 16302,
                    perfEDMTools_WizardRunFinishedEnd = 16303,
                    perfEDMTools_WizardPopulateConnectionsComboBoxBegin = 16304,
                    perfEDMTools_WizardPopulateConnectionsComboBoxEnd = 16305,
                    perfEDMTools_WizardPopulateTablesTreeBegin = 16306,
                    perfEDMTools_WizardPopulateTablesTreeEnd = 16307,
                    perfEDMTools_WizardSaveConnectionStringInAppConfigBegin = 16308,
                    perfEDMTools_WizardSaveConnectionStringInAppConfigEnd = 16309,
                    perfEDMTools_WizardDDEXDataConnectionDialogBegin = 16310,
                    perfEDMTools_WizardDDEXDataConnectionDialogEnd = 16311,
                    perfEDMTools_WizardGenerateModelBegin = 16312,
                    perfEDMTools_WizardGenerateModelEnd = 16313,
                    perfEDMTools_WizardProjectItemFinishedGenerating = 16314,
                    perfEDMTools_WizardGenerateStoreMetadataBegin = 16315,
                    perfEDMTools_WizardGenerateStoreMetadataEnd = 16316,
                    perfEDMTools_WizardGenerateModelAndMappingMetadataBegin = 16317,
                    perfEDMTools_WizardGenerateModelAndMappingMetadataEnd = 16318,
                    perfEDMTools_WizardWriteCSDLFileBegin = 16319,
                    perfEDMTools_WizardWriteCSDLFileEnd = 16320,
                    perfEDMTools_WizardWriteSSDLFileBegin = 16321,
                    perfEDMTools_WizardWriteSSDLFileEnd = 16322,
                    perfEDMTools_WizardWriteMSLFileBegin = 16323,
                    perfEDMTools_WizardWriteMSLFileEnd = 16324,
                    perfEDMTools_SingleFileGeneratorGenerateCodeBegin = 16325,
                    perfEDMTools_SingleFileGeneratorGenerateCodeEnd = 16326,
                    perfEDMTools_NewConnectionDialogAddSourcesBegin = 16327,
                    perfEDMTools_NewConnectionDialogAddSourcesEnd = 16328,
                    perfEDMTools_WizardIsAdoNetVnextProviderBegin = 16329,
                    perfEDMTools_WizardIsAdoNetVnextProviderEnd = 16330,
                    perfEDMTools_SerializationHelper_LoadModelAndDiagram_Begin = 16331,
                    perfEDMTools_SerializationHelper_LoadModelAndDiagram_End = 16332,
                    perfEDMTools_MappingEFElement_Delete_Begin = 16333,
                    perfEDMTools_MappingEFElement_Delete_End = 16334,
                    perfEDMTools_CommandSet_OnMenuDelete_Begin = 16335,
                    perfEDMTools_CommandSet_OnMenuDelete_End = 16336,
                    perfEDMTools_EFArtifact_HandleXmlModelUndoRedoCompleted_Begin = 16337,
                    perfEDMTools_EFArtifact_HandleXmlModelUndoRedoCompleted_End = 16338,
                    perfEDMTools_NewEntityDialog_OnClosing_Begin = 16339,
                    perfEDMTools_NewEntityDialog_OnClosing_End = 16340,
                    perfEDMTools_EntityDesignerDiagram_AddNewEntityType_Begin = 16341,
                    perfEDMTools_EntityDesignerDiagram_AddNewEntityType_End = 16342,
                    perfEDMTools_EntityType_ChangeAction_Invoke_Begin = 16343,
                    perfEDMTools_EntityType_ChangeAction_Invoke_End = 16344,
                    perfEDMTools_CommandSet_OnMenuValidate_Begin = 16345,
                    perfEDMTools_CommandSet_OnMenuValidate_End = 16346,
                    perfEDMTools_EdmUpdateSolutionEvents_ValidateModelAndPopulateErrorList_Begin = 16347,
                    perfEDMTools_EdmUpdateSolutionEvents_ValidateModelAndPopulateErrorList_End = 16348,
                    perfEDMTools_EFModelErrorTask_NavigateTo_Begin = 16349,
                    perfEDMTools_EFModelErrorTask_NavigateTo_End = 16350,
                    perfEDMTools_DocData_OnTransactionCommitted_Begin = 16351,
                    perfEDMTools_DocData_OnTransactionCommitted_End = 16352,
                    perfEDMTools_ColumnNameColumn_SetValue_Begin = 16353,
                    perfEDMTools_ColumnNameColumn_SetValue_End = 16354,
                    perfEDMTools_DocData_Save_Begin = 16355,
                    perfEDMTools_DocData_Save_End = 16356,
                    perfEDMTools_CommandSet_SelectAll_Begin = 16357,
                    perfEDMTools_CommandSet_SelectAll_End = 16358,
                    perfEDMTools_UpdateFromDatabaseEngine_UpdateModelFromDatabase_Begin = 16359,
                    perfEDMTools_UpdateFromDatabaseEngine_UpdateModelFromDatabase_BeginWizard = 16360,
                    perfEDMTools_UpdateFromDatabaseEngine_UpdateModelFromDatabase_EndWizard = 16361,
                    perfEDMTools_UpdateFromDatabaseEngine_UpdateModelFromDatabase_End = 16362,
                    perfEDMTools_CommandSet_OnMenuAddComplexType_Begin = 16363,
                    perfEDMTools_CommandSet_OnMenuAddComplexType_End = 16364,

                    
                    perfSharePointBDCProjectLoadModelStart = 16480,
                    perfSharePointBDCProjectLoadModelEnd = 16481,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeLucidToolsMarkers)
                    
                    perfLucidTools_LoadSchemaExplorerBegin = 16500,
                    perfLucidTools_LoadSchemaExplorerEnd = 16501,
                    perfLucidTools_SchemaExplorerChangeDisplayOptionBegin = 16502,
                    perfLucidTools_SchemaExplorerChangeDisplayOptionEnd = 16503,
                    perfLucidTools_SchemaExplorerDisplaySearchResultsBegin = 16504,
                    perfLucidTools_SchemaExplorerDisplaySearchResultsEnd = 16505,
                    perfLucidTools_SchemaExplorerCloseDocumentBegin = 16506,
                    perfLucidTools_SchemaExplorerCloseDocumentEnd = 16507,
                    perfLucidTools_SchemaExplorerResetSearchBegin = 16508,
                    perfLucidTools_SchemaExplorerResetSearchEnd = 16509,
                    perfLucidTools_SchemaExplorerSelectNodeBegin = 16510,
                    perfLucidTools_SchemaExplorerSelectNodeEnd = 16511,
                    perfLucidTools_SchemaExplorerModelUpdatedBegin = 16512,
                    perfLucidTools_SchemaExplorerModelUpdatedEnd = 16513,
                    perfLucidTools_SchemaExplorerBusyIndicatorBegin = 16514,
                    perfLucidTools_SchemaExplorerBusyIndicatorEnd = 16515,
                    perfLucidTools_GraphLayoutBegin = 16516,
                    perfLucidTools_GraphLayoutEnd = 16517,
                    perfLucidTools_GraphCalculationBegin = 16518,
                    perfLucidTools_GraphCalculationEnd = 16519,
                    perfLucidTools_ChangeWorkspaceBegin = 16520,
                    perfLucidTools_ChangeWorkspaceEnd = 16521,
                    perfLucidTools_DesignerViews_ModelUpdatedBegin = 16522,
                    perfLucidTools_DesignerViews_ModelUpdatedEnd = 16523,
                    perfLucidTools_CMV_ToggleDocumentationBegin = 16524,
                    perfLucidTools_CMV_ToggleDocumentationEnd = 16525,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeWcfToolsMarkers)
                    
                    perfWcfTools_TestHostLoad = 16700,
                    perfWcfTools_TestHostReady = 16701,
                    perfWcfTools_TestClientLoad = 16720,
                    perfWcfTools_TestClientReady = 16721,
                    perfWcfTools_ConfigIntellisenseStart = 16730,
                    perfWcfTools_ConfigIntellisenseEnd = 16731,
                    perfWcfTools_ConfigValidationStart = 16740,
                    perfWcfTools_ConfigValidationEnd = 16741,
                    perfWcfTools_CopyPasteXmlAsClrTypesStart = 16750,
                    perfWcfTools_CopyPasteXmlAsClrTypesEnd = 16751,
                    perfWcfTools_XsdBuildTaskStart = 16760,
                    perfWcfTools_XsdBuildTaskEnd = 16761,
                    perfWcfTools_ServiceExplorerStart = 16762,
                    perfWcfTools_ServiceExplorerEnd = 16763,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSDataMarkers)
                    
                    perfVSData_ServerExplorerEnumerationEnd = 16900,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeTrinityMarkers)
                    

                    
                    perfVSTODownloadAddInStart = 17000,
                    perfVSTODownloadAddInEnd = 17001,
                    perfVSTOGetManifestsStart = 17002,
                    perfVSTOGetManifestsEnd = 17003,
                    perfVSTOInstallAddInStart = 17004,
                    perfVSTOInstallAddInEnd = 17005,
                    perfVSTOValidateOfficeManifestStart = 17006,
                    perfVSTOValidateOfficeManifestEnd = 17007,
                    perfVSTOValidateVstaManifestStart = 17008,
                    perfVSTOValidateVstaManifestEnd = 17009,
                    perfVSTOAnalyzeCertificateStart = 17010,
                    perfVSTOAnalyzeCertificateEnd = 17011,
                    perfVSTOSecurityCheckStart = 17012,
                    perfVSTOSecurityCheckEnd = 17013,
                    perfVSTOOfflineProcessStart = 17014,
                    perfVSTOOfflineProcessEnd = 17015,
                    perfVSTODownloadProgressStart = 17016,
                    perfVSTODownloadProgressEnd = 17017,
                    perfVSTODownloadAppCompletedStart = 17018,
                    perfVSTODownloadAppCompletedEnd = 17019,
                    perfVSTOGetManifestCompletedStart = 17020,
                    perfVSTOGetManifestCompletedEnd = 17021,
                    perfVSTOHandleOfflineStart = 17022,
                    perfVSTOHandleOfflineEnd = 17023,
                    perfVSTOHandleOfflineGetManifestsStart = 17024,
                    perfVSTOHandleOfflineGetManifestsEnd = 17025,
                    perfVSTOVerifySecurityStart = 17026,
                    perfVSTOVerifySecurityEnd = 17027,
                    perfVSTODetermineRequestedPermissionsStart = 17028,
                    perfVSTODetermineRequestedPermissionsEnd = 17029,
                    perfVSTOSetCLRSecurityCheckOffline = 17030,
                    perfVSTOVerifySecurityCheckStart = 17031,
                    perfVSTOVerifySecurityCheckEnd = 17032,
                    perfVSTOCheckSignaturesStart = 17033,
                    perfVSTOCheckSignaturesEnd = 17034,
                    perfVSTOUninstallCustomAddInStart = 17035,
                    perfVSTOUninstallCustomAddInEnd = 17036,
                    perfVSTOInstallUpdateRollbackStart = 17037,
                    perfVSTOInstallUpdateRollbackEnd = 17038,
                    perfVSTOVerifyClickOnceSettingsStart = 17039,
                    perfVSTOVerifyClickOnceSettingsEnd = 17040,
                    perfVSTOManageCertificateStoreStart = 17041,
                    perfVSTOManageCertificateStoreEnd = 17042,
                    perfVSTOPregenerateApplicationManifestStart = 17043,
                    perfVSTOPregenerateApplicationManifestEnd = 17044,
                    perfVSTOCustomizeOffice2007DocumentStart = 17045,
                    perfVSTOCustomizeOffice2007DocumentEnd = 17046,
                    perfVSTOSetOffice2007AddInRegistrationStart = 17047,
                    perfVSTOSetOffice2007AddInRegistrationEnd = 17048,
                    perfVSTOSetInclusionListEntryStart = 17049,
                    perfVSTOSetInclusionListEntryEnd = 17050,
                    perfVSTOGenerateOfficeDocumentManifestStart = 17051,
                    perfVSTOGenerateOfficeDocumentManifestEnd = 17052,
                    perfVSTOGenerateOfficeAddInManifestStart = 17053,
                    perfVSTOGenerateOfficeAddInManifestEnd = 17054,
                    perfVSTOEmbedTypeLibraryStart = 17055,
                    perfVSTOEmbedTypeLibraryEnd = 17056,
                    perfVSTOTemporaryCertificateWizardStart = 17057,
                    perfVSTOTemporaryCertificateWizardEnd = 17058,
                    perfVSTOApplicationManifestTemplateGetXmlDocumentStart = 17059,
                    perfVSTOApplicationManifestTemplateGetXmlDocumentEnd = 17060,
                    perfVSTOApplicationManifestTemplateGetVstaManifestStart = 17061,
                    perfVSTOApplicationManifestTemplateGetVstaManifestEnd = 17062,
                    perfVSTOApplicationManifestTemplateGetEntryPointsStart = 17063,
                    perfVSTOApplicationManifestTemplateGetEntryPointsEnd = 17064,
                    perfVSTOTemporaryCertificateWizardCreatePfxStart = 17065,
                    perfVSTOTemporaryCertificateWizardCreatePfxEnd = 17066,
                    perfVSTOTemporaryCertificateWizardAddToStoreStart = 17067,
                    perfVSTOTemporaryCertificateWizardAddToStoreEnd = 17068,
                    perfVSTOTemporaryCertificateWizardChangeProjectFileAndSaveStart = 17069,
                    perfVSTOTemporaryCertificateWizardChangeProjectFileAndSaveEnd = 17070,
                    perfVSTOVerifyTrustUsingPromptKeyStart = 17071,
                    perfVSTOVerifyTrustUsingPromptKeyEnd = 17072,
                    perfVSTOCallStartupEntryPointsBegin = 17073,
                    perfVSTOCallStartupEntryPointsEnd = 17074,
                    perfVSTOInstantiateEntryPointsBegin = 17075,
                    perfVSTOInstantiateEntryPointsEnd = 17076,
                    perfVSTOBootCLRBegin = 17077,
                    perfVSTOBootCLREnd = 17078,
                    perfVSTOParseManifestBegin = 17079,
                    perfVSTOParseManifestEnd = 17080,

                    
                    perfVSTOFormRegionLoadAvailableStart = 17101,
                    perfVSTOFormRegionLoadAvailableEnd = 17102,
                    perfVSTOFormRegionLookUpInstanceStart = 17103,
                    perfVSTOFormRegionLookUpInstanceEnd = 17104,
                    perfVSTOFormRegionGetManifestStart = 17105,
                    perfVSTOFormRegionGetManifestEnd = 17106,
                    perfVSTOFormRegionGetIconStart = 17107,
                    perfVSTOFormRegionGetIconEnd = 17108,
                    perfVSTOFormRegionGetStorageStart = 17109,
                    perfVSTOFormRegionGetStorageEnd = 17110,
                    perfVSTOFormRegionBeforeShowStart = 17111,
                    perfVSTOFormRegionBeforeShowEnd = 17112,
                    perfVSTOFormRegionManifestSerializationStart = 17113,
                    perfVSTOFormRegionManifestSerializationEnd = 17114,

                    
                    perfVSTOManagedWarmupSyncStart = 17126,
                    perfVSTOManagedWarmupSyncEnd = 17127,
                    perfVSTOManagedWarmupASyncEnd = 17128,

                    
                    
                    perfSharePointExplorerExpandLocalhostStart = 17201,
                    
                    perfSharePointExplorerExpandLocalhostEnd = 17202,
                    
                    perfSharePointExplorerConnectToUrlStart = 17203,
                    
                    perfSharePointExplorerConnectToUrlEnd = 17204,
                    
                    perfSharePointExplorerExpandNodeStart = 17205,
                    
                    perfSharePointExplorerExpandNodeEnd = 17206,

                    
                    perfWorkflowProjectAttachDebuggerStart = 17231,
                    perfWorkflowProjectAttachDebuggerEnd = 17232,
                    perfWorkflowProjectDeployStart = 17233,
                    perfWorkflowProjectDeployEnd = 17234,
                    perfWorkflowProjectWakeSharePointStart = 17235,
                    perfWorkflowProjectWakeSharePointEnd = 17236,

                    
                    
                    perfSharePointWizardLaunchStart = 17251,
                    
                    perfSharePointWizardLaunchEnd = 17252,
                    
                    perfSharePointWizardPageFlipStart = 17253,
                    
                    perfSharePointWizardPageFlipEnd = 17254,

                    
                    perfSharePointDeployIisRecycleStart = 17255,
                    
                    perfSharePointDeployIisRecycleEnd = 17256,
                    
                    perfSharePointDeployRetractSolutionStart = 17257,
                    
                    perfSharePointDeployRetractSolutionEnd = 17258,
                    
                    perfSharePointDeployAddSolutionStart = 17259,
                    
                    perfSharePointDeployAddSolutionEnd = 17260,
                    
                    perfSharePointDeploySucceeded = 17261,
                    
                    perfSharePointDeployFailed = 17262,
                    
                    perfSharePointRetractSucceeded = 17263,
                    
                    perfSharePointRetractFailed = 17264,
                    
                    perfSharePointBuildStart = 17265,
                    
                    perfSharePointActivateFeaturesStart = 17266,
                    
                    perfSharePointActivateFeaturesEnd = 17267,
                    
                    perfSharePointAutoRetractAndWarmupDone = 17268,

                    
                    perfSharePointRemotePublishStart = 17269,
                    
                    perfSharePointRemotePublishEnd = 17270,
                    
                    perfSharePointRemotePublishRealStart = 17271,
                    
                    perfSharePointRemotePublishRealEnd = 17272,
                    
                    perfSharePointRetractCommandStart = 17273,

                    
                    perfSharePointRerWizardFinishStart = 17275,
                    
                    perfSharePointRerWizardFinishEnd = 17276,
                    
                    perfSharePointWorkflow4WizardFinishStart = 17277,
                    
                    perfSharePointWorkflow4WizardFinishEnd = 17278,
                    
                    perfSharePointListWizardFinishStart = 17279,
                    
                    perfSharePointListWizardFinishEnd = 17280,

                    
                    
                    perfSharePointGenerateSandboxedReferenceAssemblyStart = 17401,
                    
                    perfSharePointGenerateSandboxedReferenceAssemblyEnd = 17402,
                    
                    
                    
                    perfSharePointSilverlightWebPartCreationStart = 17409,
                    
                    perfSharePointSilverlightWebPartCreationEnd = 17410,
                    
                    perfSharePointSilverlightProjectCreationStart = 17411,
                    
                    perfSharePointSilverlightProjectCreationEnd = 17412,

                    
                    
                    perfSharePointDesignerOpenStart = 17421,
                    
                    perfSharePointDesignerOpenEnd = 17422,
                    
                    perfSharePointDesignerLoadSPDataStart = 17423,
                    
                    perfSharePointDesignerLoadSPDataEnd = 17424,
                    
                    perfSharePointDesignerLoadSiteColumnsStart = 17425,
                    
                    perfSharePointDesignerLoadSiteColumnsEnd = 17426,
                    
                    perfSharePointDesignerAsyncLoadSPDataStart = 17427,
                    
                    perfSharePointDesignerAsyncLoadSPDataEnd = 17428,

                    
                    

                    
                    perfSharePointCommandRemoveWorkflowAssociationStart = 17501,
                    
                    perfSharePointCommandRemoveWorkflowAssociationEnd = 17502,

                    
                    perfSharePointCommandAssociateWorkflowStart = 17503,
                    
                    perfSharePointCommandAssociateWorkflowEnd = 17504,
                    
                    
                    perfSharePointCommandGetListsStart = 17505,
                    
                    perfSharePointCommandGetListsEnd = 17506,

                    
                    perfSharePointCommandCreateWorkflowListStart = 17507,
                    
                    perfSharePointCommandCreateWorkflowListEnd = 17508,

                    
                    perfSharePointCommandGetMissingListTypesStart = 17509,
                    
                    perfSharePointCommandGetMissingListTypesEnd = 17510,

                    
                    perfSharePointCommandGetListUrlStart = 17511,
                    
                    perfSharePointCommandGetListUrlEnd = 17512,

                    
                    perfSharePointCommandGetListRootFolderStart = 17513,
                    
                    perfSharePointCommandGetListRootFolderEnd = 17514,

                    
                    perfSharePointCommandGetListTemplatesStart = 17515,
                    
                    perfSharePointCommandGetListTemplatesEnd = 17516,

                    
                    perfSharePointCommandDeleteListsStart = 17517,
                    
                    perfSharePointCommandDeleteListsEnd = 17518,

                    
                    perfSharePointCommandGetListIDByNameStart = 17519,
                    
                    perfSharePointCommandGetListIDByNameEnd = 17520,

                    
                    perfSharePointCommandGetListIDByUrlStart = 17521,
                    
                    perfSharePointCommandGetListIDByUrlEnd = 17522,

                    
                    perfSharePointCommandDeleteFilesStart = 17523,
                    
                    perfSharePointCommandDeleteFilesEnd = 17524,

                    
                    perfSharePointCommandFileExistsStart = 17525,
                    
                    perfSharePointCommandFileExistsEnd = 17526,

                    
                    perfSharePointCommandDeleteContentTypesByIDStart = 17527,
                    
                    perfSharePointCommandDeleteContentTypesByIDEnd = 17528,

                    
                    perfSharePointCommandDeleteContentTypesByNameStart = 17529,
                    
                    perfSharePointCommandDeleteContentTypesByNameEnd = 17530,

                    
                    perfSharePointCommandContentTypeExistsByIDStart = 17531,
                    
                    perfSharePointCommandContentTypeExistsByIDEnd = 17532,

                    
                    perfSharePointCommandContentTypeExistsByNameStart = 17533,
                    
                    perfSharePointCommandContentTypeExistsByNameEnd = 17534,

                    
                    perfSharePointCommandGetAvailableContentTypesStart = 17535,
                    
                    perfSharePointCommandGetAvailableContentTypesEnd = 17536,

                    
                    perfSharePointCommandGetAvailableFieldsStart = 17537,
                    
                    perfSharePointCommandGetAvailableFieldsEnd = 17538,

                    
                    perfSharePointCommandGetDefaultSiteStart = 17539,
                    
                    perfSharePointCommandGetDefaultSiteEnd = 17540,

                    
                    perfSharePointCommandGetFileContentsStart = 17541,
                    
                    perfSharePointCommandGetFileContentsEnd = 17542,

                    
                    perfSharePointCommandValidateSiteStart = 17543,
                    
                    perfSharePointCommandValidateSiteEnd = 17544,

                    
                    perfSharePointCommandGetWebTemplateIdsStart = 17545,
                    
                    perfSharePointCommandGetWebTemplateIdsEnd = 17546,

                    
                    perfSharePointCommandGetGroupsStart = 17547,
                    
                    perfSharePointCommandGetGroupsEnd = 17548,

                    
                    perfSharePointCommandGetLocalizedStringsStart = 17549,
                    
                    perfSharePointCommandGetLocalizedStringsEnd = 17550,

                    
                    perfSharePointCommandCompileAscxFileStart = 17551,
                    
                    perfSharePointCommandCompileAscxFileEnd = 17552,

                    
                    perfSharePointCommandGetWebIdStart = 17553,
                    
                    perfSharePointCommandGetWebIdEnd = 17554,

                    
                    perfSharePointCommandAppDeployStart = 17555,
                    
                    perfSharePointCommandAppDeployEnd = 17556,

                    
                    perfSharePointCommandAppRetractStart = 17557,
                    
                    perfSharePointCommandAppRetractEnd = 17558,

                    
                    perfSharePointCommandFindAppInstanceStart = 17559,
                    
                    perfSharePointCommandFindAppInstanceEnd = 17560,

                    
                    perfSharePointCommandGetInstallCompletionStatusStart = 17561,
                    
                    perfSharePointCommandGetInstallCompletionStatusEnd = 17562,

                    
                    perfSharePointCommandGetContentTypeFieldsCommandStart = 17563,
                    
                    perfSharePointCommandGetContentTypeFieldsCommandEnd = 17564,

                    
                    perfSharePointCommandGetListFieldsCommandStart = 17565,
                    
                    perfSharePointCommandGetListFieldsCommandEnd = 17566,

                    
                    perfSharePointCommandGetListPropertiesStart = 17567,
                    
                    perfSharePointCommandGetListPropertiesEnd = 17568,

                    
                    perfSharePointCommandGetListsCommandStart = 17569,
                    
                    perfSharePointCommandGetListsCommandEnd = 17570,

                    
                    perfSharePointCommandGetListTemplatePropertiesStart = 17571,
                    
                    perfSharePointCommandGetListTemplatePropertiesEnd = 17572,

                    
                    perfSharePointCommandGetListTemplatesCommandStart = 17573,
                    
                    perfSharePointCommandGetListTemplatesCommandEnd = 17574,

                    
                    perfSharePointCommandGetWorkflowAssociationPropertiesStart = 17575,
                    
                    perfSharePointCommandGetWorkflowAssociationPropertiesEnd = 17576,

                    
                    perfSharePointCommandGetWorkflowAssociationsCommandStart = 17577,
                    
                    perfSharePointCommandGetWorkflowAssociationsCommandEnd = 17578,

                    
                    perfSharePointCommandGetWorkflowTemplatePropertiesStart = 17579,
                    
                    perfSharePointCommandGetWorkflowTemplatePropertiesEnd = 17580,

                    
                    perfSharePointCommandGetWorkflowTemplatesCommandStart = 17581,
                    
                    perfSharePointCommandGetWorkflowTemplatesCommandEnd = 17582,

                    
                    perfSharePointCommandGetFieldPropertiesStart = 17583,
                    
                    perfSharePointCommandGetFieldPropertiesEnd = 17584,

                    
                    perfSharePointCommandGetFeaturesCommandStart = 17585,
                    
                    perfSharePointCommandGetFeaturesCommandEnd = 17586,

                    
                    perfSharePointCommandGetFeaturePropertiesStart = 17587,
                    
                    perfSharePointCommandGetFeaturePropertiesEnd = 17588,

                    
                    perfSharePointCommandGetListViewsCommandStart = 17589,
                    
                    perfSharePointCommandGetListViewsCommandEnd = 17590,

                    
                    perfSharePointCommandGetListViewPropertiesStart = 17591,
                    
                    perfSharePointCommandGetListViewPropertiesEnd = 17592,

                    
                    perfSharePointCommandGetSiteStart = 17593,
                    
                    perfSharePointCommandGetSiteEnd = 17594,

                    
                    perfSharePointCommandGetSiteByIdStart = 17595,
                    
                    perfSharePointCommandGetSiteByIdEnd = 17596,

                    
                    perfSharePointCommandGetSubSitesStart = 17597,
                    
                    perfSharePointCommandGetSubSitesEnd = 17598,

                    
                    perfSharePointCommandGetSiteCollectionPropertiesStart = 17599,
                    
                    perfSharePointCommandGetSiteCollectionPropertiesEnd = 17600,

                    
                    perfSharePointCommandGetSitePropertiesStart = 17601,
                    
                    perfSharePointCommandGetSitePropertiesEnd = 17602,

                    
                    perfSharePointCommandRetrieveSiteInfoStart = 17603,
                    
                    perfSharePointCommandRetrieveSiteInfoEnd = 17604,

                    
                    perfSharePointCommandRemoveSolutionByIdStart = 17605,
                    
                    perfSharePointCommandRemoveSolutionByIdEnd = 17606,

                    
                    perfSharePointCommandRemoveSolutionByNameStart = 17607,
                    
                    perfSharePointCommandRemoveSolutionByNameEnd = 17608,

                    
                    perfSharePointCommandRecycleAppPoolStart = 17609,
                    
                    perfSharePointCommandRecycleAppPoolEnd = 17610,

                    
                    perfSharePointCommandGetWebApplicationLocalPathStart = 17611,
                    
                    perfSharePointCommandGetWebApplicationLocalPathEnd = 17612,

                    
                    perfSharePointCommandGetFileContents2Start = 17613,
                    
                    perfSharePointCommandGetFileContents2End = 17614,

                    
                    perfSharePointCommandGetFileLastModifiedTimeStart = 17615,
                    
                    perfSharePointCommandGetFileLastModifiedTimeEnd = 17616,

                    
                    perfSharePointCommandGetStartupUrlStart = 17617,
                    
                    perfSharePointCommandGetStartupUrlEnd = 17618,

                    
                    perfSharePointCommandGetBrowserLaunchUrlStart = 17619,
                    
                    perfSharePointCommandGetBrowserLaunchUrlEnd = 17620,

                    
                    perfSharePointCommandGetAssemblyLoadLocationsStart = 17621,
                    
                    perfSharePointCommandGetAssemblyLoadLocationsEnd = 17622,

                    
                    perfSharePointCommandDoesSolutionWithIdExistStart = 17623,
                    
                    perfSharePointCommandDoesSolutionWithIdExistEnd = 17624,

                    
                    perfSharePointCommandDoesSolutionWithNameExistStart = 17625,
                    
                    perfSharePointCommandDoesSolutionWithNameExistEnd = 17626,
                    
                    
                    perfSharePointCommandAppGetInstallCompletionStatusStart = 17627,
                    
                    perfSharePointCommandAppGetInstallCompletionStatusEnd = 17628,

                    
                    perfSharePointCommandActivateFeaturesStart = 17629,
                    
                    perfSharePointCommandActivateFeaturesEnd = 17630,

                    
                    perfSharePointCommandEnsureUserCodeServiceStart = 17631,
                    
                    perfSharePointCommandEnsureUserCodeServiceEnd = 17632,

                    
                    perfSharePointCommandGetWebApplicationUrlStart = 17633,
                    
                    perfSharePointCommandGetWebApplicationUrlEnd = 17634,

                    
                    perfSharePointCommandGetSiteUrlStart = 17635,
                    
                    perfSharePointCommandGetSiteUrlEnd = 17636,

                    
                    perfSharePointCommandGetWebUrlStart = 17637,
                    
                    perfSharePointCommandGetWebUrlEnd = 17638,

                    
                    perfSharePointCommandGetWebApplicationPrefixesStart = 17639,
                    
                    perfSharePointCommandGetWebApplicationPrefixesEnd = 17640,

                    
                    perfSharePointCommandIsMasterPageReferenceEnabledStart = 17641,
                    
                    perfSharePointCommandIsMasterPageReferenceEnabledEnd = 17642,

                    
                    perfSharePointCommandGetMasterUrlStart = 17643,
                    
                    perfSharePointCommandGetMasterUrlEnd = 17644,

                    
                    perfSharePointCommandGetCustomMasterUrlStart = 17645,
                    
                    perfSharePointCommandGetCustomMasterUrlEnd = 17646,

                    
                    perfSharePointCommandGetContentTypePropertiesStart = 17647,
                    
                    perfSharePointCommandGetContentTypePropertiesEnd = 17648,

                    
                    perfSharePointCommandGetContentTypesCommandStart = 17649,
                    
                    perfSharePointCommandGetContentTypesCommandEnd = 17650,

                    
                    perfSharePointCommandInstallIisExpressCertStart = 17651,
                    
                    perfSharePointCommandInstallIisExpressCertEnd = 17652,

                    
                    perfSharePointCommandGetDefaultSite2Start = 17653,
                    
                    perfSharePointCommandGetDefaultSite2End = 17654,
                    
                    
                    perfSharePointCommandAddSolutionStart = 17655,
                    
                    perfSharePointCommandAddSolutionEnd = 17656,

                    
                    perfSharePointCommandGetListFieldsStart = 17657,
                    
                    perfSharePointCommandGetListFieldsEnd = 17658,

                    
                    perfSharePointCommandGetListTemplateSchemaStart = 17659,
                    
                    perfSharePointCommandGetListTemplateSchemaEnd = 17660,

                    
                    perfSharePointCommandUpgradeFileStart = 17661,
                    
                    perfSharePointCommandUpgradeFileEnd = 17662,

                    
                    perfSharePointCommandGetWorkflowAssociationsForWebStart = 17663,
                    
                    perfSharePointCommandGetWorkflowAssociationsForWebEnd = 17664,

                    
                    perfSharePointCommandGetWorkflowAssociationsForListStart = 17665,
                    
                    perfSharePointCommandGetWorkflowAssociationsForListEnd = 17666,

                    
                    perfSharePointCommandGetSupportedTranslationLanguagesStart = 17667,
                    
                    perfSharePointCommandGetSupportedTranslationLanguagesEnd = 17668,

                    
                    perfSharePointCommandLoadAppStart = 17669,
                    
                    perfSharePointCommandLoadAppEnd = 17670,

                    
                    perfSharePointCommandInstallAppStart = 17671,
                    
                    perfSharePointCommandInstallAppEnd = 17672,

                    
                    perfSharePointCommandCancelAllJobsStart = 17673,
                    
                    perfSharePointCommandCancelAllJobsEnd = 17674,
                    
                    
                    perfSharePointCommandUpdateWorkflowStart = 17675,
                    
                    perfSharePointCommandUpdateWorkflowEnd = 17676,
                    
                    
                    perfSharePointCommandGetAppWebListIDByUrlStart = 17677,
                    
                    perfSharePointCommandGetAppWebListIDByUrlEnd = 17678,

                    
                    perfSharePointCommandGetPartitionAndFarmNameStart = 17679,
                    
                    perfSharePointCommandGetPartitionAndFarmNameEnd = 17680,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeShellUIMarkers)
                    
                    perfGel_CreateUIElementBegin = 18000,
                    perfGel_CreateUIElementEnd = 18001,
                    perfGel_GetDataSourceBegin = 18002,
                    perfGel_GetDataSourceEnd = 18003,
                    perfGel_GetConverterBegin = 18004,
                    perfGel_GetConverterEnd = 18005,
                    perfGel_UIFactoryPackageLoad = 18006,
                    perfGel_DataSourceFactoryPackageLoad = 18007,
                    perfGel_DataConverterFactoryPackageLoad = 18008,

                    perfShellUI_CreateWpfMainFrameBegin = 18050,
                    perfShellUI_CreateWpfMainFrameEnd = 18051,
                    perfShellUI_CreateCommandModelsBegin = 18052,
                    perfShellUI_CreateCommandModelsEnd = 18053,
                    perfShellUI_UpdateCommandModelsBegin = 18054,
                    perfShellUI_UpdateCommandModelsEnd = 18055,
                    perfShellUI_CreateToolWindowBegin = 18056,
                    perfShellUI_CreateToolWindowEnd = 18057,

                    perfShellUI_LazyLocateDataAndCreateSingleModelBegin = 18058,
                    perfShellUI_LazyLocateDataAndCreateSingleModelEnd = 18059,
                    perfShellUI_LazyBuildModelsOfTypeBegin = 18060,
                    perfShellUI_LazyBuildModelsOfTypeEnd = 18061,

                    perfShellUI_CmdUpdateRequested = 18062,
                    perfShellUI_CmdUpdateForced = 18063,
                    perfShellUI_CmdUpdateFromIdle = 18064,

                    perfShellUI_CommandSearchUIThreadBoundWorkBegin = 18065,
                    perfShellUI_CommandSearchUIThreadBoundWorkEnd = 18066,

                    perfShellUI_CommandSearchCommandTextMatchBegin = 18067,
                    perfShellUI_CommandSearchCommandTextMatchEnd = 18068,

                    perfShellUI_CommandSearchCommandFilterToVisibleEnabledBegin = 18069,
                    perfShellUI_CommandSearchCommandFilterToVisibleEnabledEnd = 18070,

                    perfShellUI_CommandSearchConstructParentChainBegin = 18071,
                    perfShellUI_CommandSearchConstructParentChainEnd = 18072,

                    perfShellUI_CommandSearchMarshalToUIThreadBegin = 18073,
                    perfShellUI_CommandSearchMarshalToUIThreadEnd = 18074,

                    perfShellUI_CommandSearchRetrieveDisplayTextBegin = 18075,
                    perfShellUI_CommandSearchRetrieveDisplayTextEnd = 18076,

                    perfShellUI_CommandSearchCreateBitmapSourceBegin = 18077,
                    perfShellUI_CommandSearchCreateBitmapSourceEnd = 18078,

                    perfShellUI_CommandSearchConstructResultDataModelsBegin = 18079,
                    perfShellUI_CommandSearchConstructResultDataModelsEnd = 18080,

                    
                    perfShellUI_UpdateCommandModelsInterrupted = 18081,

                    
                    perfShellUI_ToolboxInitContentBegin = 18082,
                    
                    perfShellUI_ToolboxInitContentEnd = 18083,

                    
                    perfShellUI_VsInputProcessingMsgReceived = 18084,
                    
                    perfShellUI_VsInputProcessingMsgAcknowledged = 18085,

                    
                    perfShellUI_EnteringMenuMode = 18086,
                    
                    perfShellUI_LeavingMenuMode = 18087,

                    
                    perfShellUI_ThemeBitmapSourceBegin = 18088,
                    
                    perfShellUI_ThemeBitmapSourceEnd = 18089,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeShellUIManagedMarkers)
                    
                    perfGel_ShapeIdentifierCacheMiss = 18100,
                    perfGel_ShapeIdentifierCacheDiscard = 18101,

                    
                    perfShellUI_NotificationsUpdateExpirationBegin = 18102,
                    
                    perfShellUI_NotificationsUpdateExpirationEnd = 18103,
                    
                    perfShellUI_NotificationsUpdateCreationBegin = 18104,
                    
                    perfShellUI_NotificationsUpdateCreationEnd = 18105,
                    
                    perfShellUI_NotificationsAddBegin = 18106,
                    
                    perfShellUI_NotificationsAddEnd = 18107,
                    
                    perfShellUI_NotificationsRemoveBegin = 18108,
                    
                    perfShellUI_NotificationsRemoveEnd = 18109,
                    
                    perfShellUI_TaskLibrary_TaskCreated = 18110,
                    perfShellUI_TaskLibrary_TaskDependencyCreated = 18111,
                    perfShellUI_TaskLibrary_TaskStateChanged = 18112,
                    perfShellUI_TaskLibrary_TaskDescription = 18113,
                    perfShellUI_TaskLibrary_Wait = 18114,
                    
                    
                    perfShellUI_ActiveViewSwitchBegin = 18115,
                    
                    perfShellUI_ActiveViewSwitchEnd = 18116,

                    perfRoamingSettings_ApplyOneClientBegin = 18120,
                    perfRoamingSettings_ApplyOneClientEnd = 18121,
                    perfRoamingSettings_ApplyMultipleClientsBegin = 18122,
                    perfRoamingSettings_ApplyMultipleClientsEnd = 18123,
                    perfRoamingSettings_GetAndUploadClientStateBegin = 18124,
                    perfRoamingSettings_GetAndUploadClientStateEnd = 18125,
                    perfRoamingSettings_PollProfileClientBegin = 18126,
                    perfRoamingSettings_PollProfileClientEnd = 18127,
                    perfRoamingSettings_ProfileClientUIThreadImportBegin = 18128,
                    perfRoamingSettings_ProfileClientUIThreadImportEnd = 18129,
                    perfRoamingSettings_PollProfileClientUIThreadPartBegin = 18130,
                    perfRoamingSettings_PollProfileClientUIThreadPartEnd = 18131,
                    perfRoamingSettings_ProfileClientUIThreadGetSettingsFilesBegin = 18132,
                    perfRoamingSettings_ProfileClientUIThreadGetSettingsFilesEnd = 18133,
                    perfRoamingSettings_ProfileClientUIThreadAddBrowseFileBegin = 18134,
                    perfRoamingSettings_ProfileClientUIThreadAddBrowseFileEnd = 18135,
                    perfRoamingSettings_ProfileClientUIThreadGetSettingsTreeBegin = 18136,
                    perfRoamingSettings_ProfileClientUIThreadGetSettingsTreeEnd = 18137,

                    
                    perfShellUI_WindowSearchInitiated = 18140,
                    
                    perfShellUI_GlobalSearchProviderSearchInitiated = 18141,
                    
                    perfShellUI_GlobalSearchProviderSearchStarted = 18142,
                    
                    perfShellUI_GlobalSearchProviderSearchStopped = 18143,
                    
                    perfShellUI_GlobalSearchProviderSearchCompleted = 18144,
                    
                    perfShellUI_WindowSearchCleared = 18145,
                    
                    perfShellUI_GlobalSearchLoadProvidersBegin = 18146,
                    
                    perfShellUI_GlobalSearchLoadProvidersEnd = 18147,
                    
                    perfShellUI_HierarchyItemInvokeBegin = 18148,
                    
                    perfShellUI_HierarchyItemInvokeEnd = 18149,
                    perfShellUI_MainWindowCtor = 18150,
                    perfShellUI_MainWindowOnSourceInitialized = 18151,
                    perfShellUI_LoadProfileFromLocalStorageBegin = 18152,
                    perfShellUI_LoadProfileFromLocalStorageEnd = 18153,
                    perfShellUI_SaveProfileToLocalStorageBegin = 18154,
                    perfShellUI_SaveProfileToLocalStorageEnd = 18155,
                    perfShellUI_LoadDefaultWindowProfileBegin = 18156,
                    perfShellUI_LoadDefaultWindowProfileEnd = 18157,
                    perfShellUI_CreateProfileFromRegistryBegin = 18158,
                    perfShellUI_CreateProfileFromRegistryEnd = 18159,
                    perfShellUI_ViewManagerInitializeBegin = 18160,
                    perfShellUI_ViewManagerInitializeEnd = 18161,
                    perfShellUI_BaseToolbarHostAddToolbarBegin = 18162,
                    perfShellUI_BaseToolbarHostAddToolbarEnd = 18163,
                    perfShellUI_UtilitySelectStyleForItemBegin = 18164,
                    perfShellUI_UtilitySelectStyleForItemEnd = 18165,
                    perfShellUI_GrayscaleImageConvertValueBegin = 18166,
                    perfShellUI_GrayscaleImageConvertValueEnd = 18167,
                    perfShellUI_UtilityResolveDeferredStyleForItem = 18168,
                    
                    perfShellUI_GlobalSearchStarted = 18169,
                    
                    perfShellUI_GlobalSearchStopped = 18170,
                    
                    perfShellUI_GlobalSearchCompleted = 18171,
                    
                    perfShellUI_GlobalSearchUIUpdated = 18172,
                    
                    perfShellUI_WindowSearchStarted = 18173,
                    
                    perfShellUI_WindowSearchStopped = 18174,
                    
                    perfShellUI_WindowSearchCompleted = 18175,
                    perfShellUI_DockOperationUndockBegin = 18176,
                    perfShellUI_DockOperationUndockEnd = 18177,
                    perfShellUI_DockOperationFloatBegin = 18178,
                    perfShellUI_DockOperationFloatEnd = 18179,
                    perfShellUI_DockOperationDockBegin = 18180,
                    perfShellUI_DockOperationDockEnd = 18181,
                    perfShellUI_DockOperationSnapToBookmarkBegin = 18182,
                    perfShellUI_DockOperationSnapToBookmarkEnd = 18183,
                    perfShellUI_DockOperationReorderTabBegin = 18184,
                    perfShellUI_DockOperationReorderTabEnd = 18185,
                    perfShellUI_DockOperationAutoHideBegin = 18186,
                    perfShellUI_DockOperationAutoHideEnd = 18187,
                    perfChooseToolboxItems_ItemListPopulationComplete = 18188,
                    perfChooseToolboxItems_ClickOkBegin = 18189,
                    perfChooseToolboxItems_ClickOkEnd = 18190,
                    perfChooseToolboxItems_RefreshLegacyPageBegin = 18191,
                    perfChooseToolboxItems_RefreshLegacyPageEnd = 18192,
                    
                    perfShellUI_WindowSearchSetupStarted = 18193,
                    
                    perfShellUI_WindowSearchSetupCompleted = 18194,
                    
                    perfShellUI_SearchMRUPopulationStarted = 18195,
                    
                    perfShellUI_SearchMRUPopulationCompleted = 18196,
                    
                    perfShellUI_HierarchyCollectionLoadStarted = 18197,
                    
                    perfShellUI_HierarchyCollectionLoadCompleted = 18198,
                    
                    perfShellUI_HierarchyAsyncExpansionCompleted = 18199,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSExpressMarkers)
                    
                    perfVSExpress_GettingStartedBegin = 18200,
                    perfVSExpress_GettingStartedShortcutResolved = 18201,
                    perfVSExpress_GettingStartedDocumentLoaded = 18202,
                    perfVSNewProjectDlgBegin = 18203,
                    perfVSNewProjectDlgOnlineCategoryBegin = 18204,
                    perfVSNewProjectDlgOnlineCategoryEnd = 18205,
                    perfVSNewProjectDlgOnlinePageBegin = 18206,
                    perfVSNewProjectDlgOnlinePageEnd = 18207,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSProToolsMarkers)
                    
                    
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeTestExplorerMarkers)
                    
                    perfTestExplorer_BeginTestRun = 18500,
                    perfTestExplorer_EndTestRun = 18501,
                    perfTestExplorer_BeginTestRunUpdate = 18502,
                    perfTestExplorer_EndTestRunUpdate = 18503,
                    perfTestExplorer_BeginTestRunComplete = 18504,
                    perfTestExplorer_EndTestRunComplete = 18505,
                    perfTestExplorer_BeginTestDiscovery = 18506,
                    perfTestExplorer_EndTestDiscovery = 18507,
                    perfTestExplorer_BeginTestDiscoveryUpdate = 18508,
                    perfTestExplorer_EndTestDiscoveryUpdate = 18509,
                    perfTestExplorer_BeginTestDiscoveryComplete = 18510,
                    perfTestExplorer_EndTestDiscoveryComplete = 18511,
                    
                    perfTestExplorer_OnTestImpactChanged = 18512,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeSEFilteringMarkers)
                    
                    perfSEFiltering_BeginEnableFilter = 18600,
                    perfSEFiltering_EndEnableFilter = 18601,
                    perfSEFiltering_BeginDisableFilter = 18602,
                    perfSEFiltering_EndDisableFilter = 18603,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeShellUIManagedMarkers)
                    
                    
                    perfShellUI_NotificationsSaveBegin = 18700,
                    
                    perfShellUI_NotificationsSaveEnd = 18701,
                    
                    perfShellUI_NotificationsLoadBegin = 18702,
                    
                    perfShellUI_NotificationsLoadEnd = 18703,
                    
                    perfShellUI_NotificationsInitialized = 18704,
                    
                    perfShellUI_NotificationsRemoveAllBegin = 18705,
                    
                    perfShellUI_NotificationsRemoveAllEnd = 18706,
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeProgressionMarkers)
                    

                    perfProgression_PackageLoadBegin = 20000,
                    perfProgression_PackageLoadEnd = 20001,

                    perfProgression_VSProviderProcessingBegin = 20002,
                    perfProgression_VSProviderProcessingEnd = 20003,

                    perfProgression_MetricsProcessingBegin = 20004,
                    perfProgression_MetricsProcessingEnd = 20005,

                    perfProgression_NestedStackBegin = 20006,
                    perfProgression_NestedStackEnd = 20007,

                    perfProgression_DependencyMatrixBegin = 20008,
                    perfProgression_DependencyMatrixEnd = 20009,

                    perfProgression_TreeViewBegin = 20010,

                    perfProgression_TreeViewEnd = 20011,


                    perfProgression_CascadeBusyBegin = 20012,

                    perfProgression_CascadeBusyEnd = 20013,

                    perfProgression_PhoenixProviderProcessingBegin = 20014,

                    perfProgression_PhoenixProviderProcessingEnd = 20015,

                    perfProgression_GraphControlProcessingBegin = 20016,

                    perfProgression_GraphControlProcessingEnd = 20017,

                    perfProgression_VirtualCanvasUpdateVisualsEnd = 20018,

                    perfProgression_GestureScale = 20019,
                    perfProgression_GestureTranslate = 20020,

                    perfProgression_Service_QueryExecuteBegin = 20021,
                    perfProgression_Service_QueryExecuteEnd = 20022,

                    perfProgression_StandardGraph_OnIdle = 20023,
                    perfProgression_StandardGraph_OnBusy = 20024,

                    perfProgression_GraphControl_OnIdle = 20025,
                    perfProgression_GraphControl_OnBusy = 20026,

                    perfProgression_GraphControl_RevealGenerationStart = 20027,
                    perfProgression_GraphControl_RevealGenerationEnd = 20028,

                    perfProgression_GraphControl_GotoDefinitionEnd = 20029,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeTeamTestMarkers)

                    
                    
                    perfTeamTest_ManualTestCaseStart_Begin = 20300,
                    perfTeamTest_ManualTestCaseStart_End = 20301,
                    perfTeamTest_ManualTestCaseEnd_Begin = 20302,
                    perfTeamTest_ManualTestCaseEnd_End = 20303,
                    perfTeamTest_EnvironmentRefreshStart = 20304,
                    perfTeamTest_EnvironmentRefreshEnd = 20305,
                    perfTeamTest_CommandLineBegin = 20306,
                    perfTeamTest_CommandLineEnd = 20307,
                    perfTeamTest_TestExecutionManagerCreateAndExecuteTestRunBegin = 20308,
                    perfTeamTest_TestViewOpenBegin = 20309,
                    perfTeamTest_TestRunEndInternalBegin = 20310,
                    perfTeamTest_TestRunEndInternalEnd = 20311,
                    perfTeamTest_ControllerProxyQueueTestRunToController = 20312,
                    perfTeamTest_LocalControllerProxyQueueTestRun = 20313,
                    perfTeamTest_TmiTestRunStarted = 20314,
                    perfTeamTest_TmiLoadTestsBegin = 20315,
                    perfTeamTest_TmiLoadTestsTipLoadDone = 20316,
                    perfTeamTest_TmiLoadTestsEnd = 20317,
                    perfTeamTest_AgentProcessMainBegin = 20318,
                    perfTeamTest_AgentProcessMainEnd = 20319,
                    perfTeamTest_UnitTestAdapterCreateAppDomainBegin = 20320,
                    perfTeamTest_UnitTestAdapterCreateAppDomainEnd = 20321,
                    perfTeamTest_TmiCtorBegin = 20322,
                    perfTeamTest_TmiCtorEnd = 20323,
                    perfTeamTest_TCMUIPackageInitializeBegin = 20324,
                    perfTeamTest_TCMUIPackageInitializeEnd = 20325,
                    perfTeamTest_TCMUIPackageCtorBegin = 20326,
                    perfTeamTest_TmiRefreshTestsBegin = 20327,
                    perfTeamTest_TmiRefreshTestsEnd = 20328,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeTeamArchMarkers)
                    

                    perfUmlModelExplorerExpandBegin = 20500,
                    perfUmlModelExplorerCollapseBegin = 20501,
                    perfUmlModelExplorerRefreshEnd = 20502,

                    perfSequenceGenerationQueryMethodNodeBegin = 20503,
                    perfSequenceGenerationQueryMethodNodeEnd = 20504,
                    perfSequenceGenerationCreateDiagramBegin = 20505,
                    perfSequenceGenerationCreateDiagramEnd = 20506,
                    perfSequenceGenerationInstanceTrackingBegin = 20507,
                    perfSequenceGenerationInstanceTrackingEnd = 20508,
                    perfSequenceGenerationRenderingBegin = 20509,
                    perfSequenceGenerationRenderingEnd = 20510,
                    perfSequenceGenerationExpandOutgoingBegin = 20511,
                    perfSequenceGenerationExpandOutgoingEnd = 20512,

            perfUmlModelExplorerScrollBegin = 20513,
                    perfUmlModelExplorerScrollEnd = 20514,

                    perfTeamArch_ValidateBegin = 20525,
                    perfTeamArch_ValidateEnd = 20526,
                    perfTeamArch_LoadArchitectureBegin = 20527,
                    perfTeamArch_LoadArchitectureEnd = 20528,
                    perfTeamArch_ValidateArchitectureBegin = 20529,

                    perfTeamArch_ValidateArchitectureEnd = 20532,
                    perfTeamArch_GenerateDependenciesStart = 20533,
                    perfTeamArch_GenerateDependenciesEnd = 20534,



#endif 


#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludePDTDebugger)
                    

                    perfPDTDebuggerRefreshBegin = 20800,
                    perfPDTDebuggerRefreshEnd = 20801,
                    perfPDTDebuggerBackendQueryStart = 20802,
                    perfPDTDebuggerBackendQueryEnd = 20803,
                    perfPDTDebuggerTasksRefreshViewStart = 20804,
                    perfPDTDebuggerTasksRefreshViewEnd = 20805,
                    perfPDTDebuggerGetTaskProviderStart = 20806,
                    perfPDTDebuggerGetTaskProviderEnd = 20807,
                    perfPDTDebuggerUILoadTaskStart = 20808,
                    perfPDTDebuggerUILoadTaskEnd = 20809,
                    perfPDTDebuggerTasksWindowGenerationStart = 20810,
                    perfPDTDebuggerTasksWindowGenerationEnd = 20811,
                    perfPDTDebuggerStacksWindowGenerationStart = 20812,
                    perfPDTDebuggerStacksWindowGenerationEnd = 20813,
                    perfPDTDebuggerTasksWindowWPFArrangeStart = 20814,
                    perfPDTDebuggerTasksWindowWPFArrangeEnd = 20815,
                    perfPDTDebuggerStacksWindowWPFArrangeStart = 20816,
                    perfPDTDebuggerStacksWindowWPFArrangeEnd = 20817,
                    perfPDTDebuggerPWatchGroupEEStart = 20818,
                    perfPDTDebuggerPWatchGroupEEEnd = 20819,

#endif 


#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeDSLMarkers)
                    

                    perfModBusBrokenReferenceResolverBegin = 21000,
                    perfModBusBrokenReferenceResolverEnd = 21001,


#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeDacMarkers)

                    
                    perfDac_ImportDacBegin = 21200,
                    perfDac_ImportDac_DacDecompiled = 21201,
                    perfDac_ImportDac_PolicyImported = 21202,
                    perfDac_ImportDac_ScriptExtracted = 21203,
                    perfDac_ImportDacEnd = 21204,
                    perfDac_DacDeployBegin = 21205,
                    perfDac_DacDeploy_DacDecompiled = 21206,
                    perfDac_DacDeployEnd = 21207,
                    perfDac_BuildBegin = 21208,
                    perfDac_BuildEnd = 21209,
                    perfDac_QueryExecutionBegin = 21210,
                    perfDac_QueryExecutionEnd = 21211,
                    perfDac_GenerateAutoCompletionListBegin = 21212,
                    perfDac_GenerateAutoCompletionListEnd = 21213,
                    perfDac_MetaDataProviderCreationBegin = 21214,
                    perfDac_MetaDataProviderCreationEnd = 21215,
            perfDac_ImportDac_DatabaseValidated = 21216,

#endif 


#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSTFMarkers)

                    
                    

                    
                    
                    perfVSTF_SCCCheckinBegin = 21300,
                    
                    perfVSTF_SCCCheckinEnd = 21301,
                    
                    perfVSTF_SCCGetBegin = 21302,
                    
                    perfVSTF_SCCGetEnd = 21303,
                    
                    perfVSTF_SCCShowNodeBegin = 21304,
                    
                    perfVSTF_SCCShowNodeEnd = 21305,
                    
                    perfVSTF_TeamNavPendingChangesRefreshBegin = 21306,
                    
                    perfVSTF_TeamNavPendingChangesRefreshEnd = 21307,
                    
                    perfVSTF_TeamNavPendingChangesCheckinBegin = 21308,
                    
                    perfVSTF_TeamNavPendingChangesCheckinEnd = 21309,
                    
                    perfVSTF_TeamNavPendingChangesChangesRefreshBegin = 21310,
                    
                    perfVSTF_TeamNavPendingChangesChangesRefreshEnd = 21311,
                    
                    perfVSTF_TeamNavMyWorkRefreshStart = 21312,
                    
                    perfVSTF_TeamNavMyWorkRefreshEnd = 21313,
                    
                    perfVSTF_TeamNavPendingChangesChangesChanged = 21314,
                    
                    perfVSTF_LocalWorkspaceSnapshotBackupStart = 21315,
                    
                    perfVSTF_LocalWorkspaceSnapshotBackupEnd = 21316,

                    
                    perfVSTF_TeamNavWorkItemsRefreshBegin = 21600,             
                    perfVSTF_TeamNavWorkItemsRefreshEnd = 21601,               
                    perfVSTF_TeamNavWorkItemsUIReady = 21602,                  

                    
                    perfVSTF_TeamNavDocumentsRefreshBegin = 21900,             
                    perfVSTF_TeamNavDocumentsRefreshEnd = 21901,               
                    perfVSTF_TeamNavDocumentsUIReady = 21902,                  

                    perfVSTF_TeamNavReportsRefreshBegin = 21903,               
                    perfVSTF_TeamNavReportsRefreshEnd = 21904,                 
                    perfVSTF_TeamNavReportsUIReady = 21905,                    


                    

                    

                    
                    

                    
                    perfVSTF_BeginPrePublishValidation = 22550,          
                    perfVSTF_EndPrePublishValidation = 22551,            
                    perfVSTF_BeginIsProjectMapped = 22552,               
                    perfVSTF_EndIsProjectMapped = 22553,                 
                    perfVSTF_BeginProjectSave = 22554,                   
                    perfVSTF_EndProjectSave = 22555,                     
                    perfVSTF_BeginProjectOpen = 22556,                   
                    perfVSTF_EndProjectOpen = 22557,                     

                    
                    
                    perfVSTF_ELeadTeamProjectShowNodeBegin = 22800,
                    
                    perfVSTF_ELeadTeamProjectShowNodeEnd = 22801,
                    
                    perfVSTF_ELeadFavoritesShowNodeBegin = 22802,
                    
                    perfVSTF_ELeadFavoritesShowNodeEnd = 22803,
                    
                    perfVSTF_ELeadDocumentShowNodeBegin = 22804,
                    
                    perfVSTF_ELeadDocumentShowNodeEnd = 22805,
                    
                    perfVSTF_ELeadTeamExplorerShowNodeBegin = 22806,
                    
                    perfVSTF_ELeadTeamExplorerShowNodeEnd = 22807,
                    
                    perfVSTF_ELeadOfficeAddinLoadingBegin = 22808,
                    
                    perfVSTF_ELeadOfficeAddinLoadingEnd = 22809,
                    
                    perfVSTF_ELeadPowerPointStencilLoadingBegin = 22810,
                    
                    perfVSTF_ELeadPowerPointStencilLoadingEnd = 22811,
                    
                    perfVSTF_ELeadPowerPointControlLoadingBegin = 22812,
                    
                    perfVSTF_ELeadPowerPointControlLoadingEnd = 22813,
                    
                    perfVSTF_ELeadOfficeAddInSearchBegin = 22814,
                    
                    perfVSTF_ELeadOfficeAddInSearchEnd = 22815,
                    
                    perfVSTF_ELeadOfficeApplicationLaunchBegin = 22816,
                    
                    perfVSTF_ELeadOfficeApplicationLaunchEnd = 22817,
                    
                    perfVSTF_ELeadOutOfProcessHostLaunchBegin = 22818,
                    
                    perfVSTF_ELeadOutOfProcessHostLaunchEnd = 22819,
                    
                    perfVSTF_ELeadOutOfProcessHostCallBegin = 22820,
                    
                    perfVSTF_ELeadOutOfProcessHostCallEnd = 22821,
                    
                    perfVSTF_ELeadSharePointDocumentLaunchBegin = 22822,
                    
                    perfVSTF_ELeadSharePointDocumentLaunchEnd = 22823,
                    
                    perfVSTF_ELeadOfficePendingRequestBegin = 22824,
                    
                    perfVSTF_ELeadOfficePendingRequestEnd = 22825,
                    
                    perfVSTF_ELeadOfficeConnectToProjectBegin = 22826,
                    
                    perfVSTF_ELeadOfficeConnectToProjectEnd = 22827,
 
 
                    
                    perfVSTF_TeamNavConnectionPageRefreshBegin = 23000,            
                    perfVSTF_TeamNavConnectionPageRefreshEnd = 23001,              
                    perfVSTF_TeamNavConnectionUIReady = 23002,                     
                    perfVSTF_TeamNavNavigateToBegin = 23003,                       
                    perfVSTF_TeamNavNavigateToEnd = 23004,                         


                    

#endif 
                    
                    
                    

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeBrowserToolsMarkers)

                    

                    
                    perfBrowserTools_LanguageServiceOutliningUpdated = 23500,
                    perfBrowserTools_LanguageServiceErrorsUpdated = 23501,
                    perfBrowserTools_LanguageServiceExternalSourcesUpdated = 23502,
                    perfBrowserTools_LanguageServiceStatementCompletionReady = 23503,
                    perfBrowserTools_LanguageServiceSignatureHelpReady = 23504,
                    perfBrowserTools_LanguageServiceOutliningTaggerBegin = 23505,
                    perfBrowserTools_LanguageServiceOutliningTaggerEnd = 23506,
                    perfBrowserTools_LanguageServiceWinRTReferenceReady = 23507,
                    perfBrowserTools_LanguageServiceGoToDefinitionReady = 23508,
                    perfBrowserTools_LanguageServiceIdle = 23509,
                    perfBrowserTools_LanguageServiceSnippetManagerInitialized = 23510,
                    perfBrowserTools_LanguageServiceFormattingBegin = 23511,
                    perfBrowserTools_LanguageServiceFormattingEnd = 23512,

                    
                    perfBrowserTools_VisualProfilerDetailsPaneLoaded = 23565,
                    perfBrowserTools_VisualProfilerGraphCollapsed = 23566,
                    perfBrowserTools_VisualProfilerGraphExpanded = 23567,
                    perfBrowserTools_VisualProfilerGridRowSelected = 23568,
                    perfBrowserTools_VisualProfilerGridScrolled = 23569,
                    perfBrowserTools_VisualProfilerGridUpdatedForTimeSelection = 23570,
                    perfBrowserTools_VisualProfilerResetZoomFinished = 23571,
                    perfBrowserTools_VisualProfilerResetZoomStarted = 23572,
                    perfBrowserTools_VisualProfilerResultsLoaded = 23573,
                    perfBrowserTools_VisualProfilerSortFinishedOnGrid = 23574,
                    perfBrowserTools_VisualProfilerSortInvokedOnGrid = 23575,
                    perfBrowserTools_VisualProfilerUserSelectedTimeSlice = 23576,
                    perfBrowserTools_VisualProfilerZoomInFinished = 23577,
                    perfBrowserTools_VisualProfilerZoomInStarted = 23578,

                    
                    perfBrowserTools_MemoryAnalyzerIdle = 23590,
                    perfBrowserTools_MemoryAnalyzerWindowClose = 23591,
                    perfBrowserTools_MemoryAnalyzerSessionEnd = 23592,

                    
                    perfBrowserTools_ProjectServiceReferencesUpdated = 23596,
                    perfBrowserTools_ProjectServiceRefreshWindowsWebApplicationDisallowed = 23597,
                    perfBrowserTools_ProjectServiceRefreshWindowsWebApplicationBegin = 23598,
                    perfBrowserTools_ProjectServiceRefreshWindowsWebApplicationEnd = 23599,

                    
                    perfBrowserTools_DiagnosticsToolWindowsPackageLoadBegin = 23600,
                    perfBrowserTools_DiagnosticsToolWindowsPackageLoadEnd = 23601,
                    perfBrowserTools_DiagnosticsToolWindowsUIContextSet = 23602,
                    perfBrowserTools_DiagnosticsToolWindowsConsoleCreated = 23603,
                    perfBrowserTools_DiagnosticsToolWindowsDomExplorerCreated = 23604,
                    perfBrowserTools_DiagnosticsToolWindowsShowConsoleBegin = 23605,
                    perfBrowserTools_DiagnosticsToolWindowsShowConsoleEnd = 23606,
                    perfBrowserTools_DiagnosticsToolWindowsShowDomExplorerBegin = 23607,
                    perfBrowserTools_DiagnosticsToolWindowsShowDomExplorerEnd = 23608,
                    perfBrowserTools_DiagnosticsToolWindowsConsoleReady = 23609,
                    perfBrowserTools_DiagnosticsToolWindowsDomExplorerReady = 23610,
                    perfBrowserTools_DiagnosticsToolWindowsExpandConsoleObjectBegin = 23611,
                    perfBrowserTools_DiagnosticsToolWindowsExpandConsoleObjectEnd = 23612,
                    perfBrowserTools_DiagnosticsToolWindowsExpandConsoleObjectInteractive = 23613,
                    perfBrowserTools_DiagnosticsToolWindowsConsoleEvalBegin = 23614,
                    perfBrowserTools_DiagnosticsToolWindowsConsoleEvalEnd = 23615,
                    perfBrowserTools_DiagnosticsToolWindowsDataTreeToggleBegin = 23616,
                    perfBrowserTools_DiagnosticsToolWindowsDataTreeToggleEnd = 23617,
                    perfBrowserTools_DiagnosticsToolWindowsTreeViewToggleBegin = 23618,
                    perfBrowserTools_DiagnosticsToolWindowsTreeViewToggleEnd = 23619,
                    perfBrowserTools_DiagnosticsToolWindowsDomExplorerRefreshBegin = 23620,
                    perfBrowserTools_DiagnosticsToolWindowsDomExplorerRefreshEnd = 23621,
                    perfBrowserTools_DiagnosticsToolWindowsDomExplorerAttributeChanged = 23622,
                    perfBrowserTools_DiagnosticsToolWindowsDomExplorerTabChanged = 23623,
                    perfBrowserTools_DiagnosticsJavaScriptCriticalErrorOccurred = 23624,

                    
                    
                    perfBrowserTools_LanguageServiceBackendHurryCalled = 23664,
                    perfBrowserTools_LanguageServiceBackendAbortCalled = 23665,
                    perfBrowserTools_LanguageServiceBackendApplyCommentsBegin = 23666,
                    perfBrowserTools_LanguageServiceBackendApplyCommentsEnd = 23667,
                    perfBrowserTools_LanguageServiceBackendExecuteAsyncRequestsBegin = 23668,
                    perfBrowserTools_LanguageServiceBackendExecuteAsyncRequestsEnd = 23669,
                    perfBrowserTools_LanguageServiceBackendGetAsyncRequestsBegin = 23670,
                    perfBrowserTools_LanguageServiceBackendGetAsyncRequestsEnd = 23671,
                    perfBrowserTools_LanguageServiceBackendGetASTSubTreeBegin = 23672,
                    perfBrowserTools_LanguageServiceBackendGetASTSubTreeEnd = 23673,
                    perfBrowserTools_LanguageServiceBackendGetASTCursorBegin = 23674,
                    perfBrowserTools_LanguageServiceBackendGetASTCursorEnd = 23675,
                    perfBrowserTools_LanguageServiceBackendGetDefinitionLocationBegin = 23676,
                    perfBrowserTools_LanguageServiceBackendGetDefinitionLocationEnd = 23677,
                    perfBrowserTools_LanguageServiceBackendParsingBegin = 23678,
                    perfBrowserTools_LanguageServiceBackendParsingEnd = 23679,
                    perfBrowserTools_LanguageServiceBackendByteCodeGenerationBegin = 23680,
                    perfBrowserTools_LanguageServiceBackendByteCodeGenerationEnd = 23681,
                    perfBrowserTools_LanguageServiceBackendExecutionBegin = 23682,
                    perfBrowserTools_LanguageServiceBackendExecutionEnd = 23683,
                    perfBrowserTools_LanguageServiceBackendRewriteTreeBegin = 23684,
                    perfBrowserTools_LanguageServiceBackendRewriteTreeEnd = 23685,
                    perfBrowserTools_LanguageServiceBackendGetMessageSetBegin = 23686,
                    perfBrowserTools_LanguageServiceBackendGetMessageSetEnd = 23687,
                    perfBrowserTools_LanguageServiceBackendGetFunctionHelpBegin = 23688,
                    perfBrowserTools_LanguageServiceBackendGetFunctionHelpEnd = 23689,
                    perfBrowserTools_LanguageServiceBackendGetASTBegin = 23690,
                    perfBrowserTools_LanguageServiceBackendGetASTEnd = 23691,
                    perfBrowserTools_LanguageServiceBackendGetQuickInfoBegin = 23692,
                    perfBrowserTools_LanguageServiceBackendGetQuickInfoEnd = 23693,
                    perfBrowserTools_LanguageServiceBackendGetCompletionsBegin = 23694,
                    perfBrowserTools_LanguageServiceBackendGetCompletionsEnd = 23695,
                    perfBrowserTools_LanguageServiceBackendGetRegionsBegin = 23696,
                    perfBrowserTools_LanguageServiceBackendGetRegionsEnd = 23697,
                    perfBrowserTools_LanguageServiceBackendUpdateBegin = 23698,
                    perfBrowserTools_LanguageServiceBackendUpdateEnd = 23699,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSDebugger)
        
                    perfVSDebuggerStartExpressionEvaluation = 23700,
                    perfVSDebuggerFinishExpressionEvaluation = 23701,
                    perfVsDebuggerImmersiveSimulatorLaunchBegin = 23702,
                    perfVsDebuggerImmersiveSimulatorLaunchEnd = 23703,
                    perfVsDebuggerImmersiveSimulatorDebuggeeLaunch = 23704,
                    perfVsDebuggerCoreSessionEnd = 23705,
                    perfVsDebuggerStartWithoutDebuggingEnd = 23706,

                    perfVsDebuggerDTEStepOverBegin = 23707,
                    perfVsDebuggerDTEStepOverEnd = 23708,
                    perfVsDebuggerDTEStepIntoBegin = 23709,
                    perfVsDebuggerDTEStepIntoEnd = 23710,
                    perfVsDebuggerManagedHeapStartHeapWalk = 23711,
                    perfVsDebuggerManagedHeapEndHeapWalk = 23712,
                    perfVsDebuggerManagedHeapStartGetRows = 23713,
                    perfVsDebuggerManagedHeapEndGetRows = 23714,
        
#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSNativeMarkers)

                    

                    perfAppxEditorOpenBegin = 23751,
                    perfAppxEditorOpenEnd = 23752,
					
                    perfAppxPublishWizardOpenBegin = 23753,
                    perfAppxPublishWizardOpenEnd = 23754,
                    perfAppxPublishWizardFinalizeBegin = 23755,
                    perfAppxPublishWizardFinalizeEnd = 23756,

                    perfAppxPushNotificationWizardOpenBegin = 23757,
                    perfAppxPushNotificationWizardOpenEnd = 23758,
                    perfAppxPushNotificationHandlerAddBegin = 23759,
                    perfAppxPushNotificationHandlerAddEnd = 23760,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeCommonProjectSystemMarkers)

                    

                    perfCpsProjectLoadSourceItemsPopulated = 23902,
                    perfCpsProjectLoadReferencesPopulated = 23903,

                    perfCpsProjectAddItemBegin = 23904,
                    perfCpsProjectAddItemEnd = 23905,
                    perfCpsProjectIncludeInProjectBegin = 23906,
                    perfCpsProjectIncludeInProjectEnd = 23907,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeTailoredWizardMarkers)
                    
                    
                    
                    perfTailoredWizardLaunchBegin = 24000,
                    perfTailoredWizardLaunchEnd = 24001,
                    perfTailoredWizardManifestEditBegin = 24002,
                    perfTailoredWizardManifestEditEnd = 24003,
                    perfTailoredWizardAppActivationBegin = 24004,
                    perfTailoredWizardAppActivationEnd = 24005,
                    perfTailoredWizardAppActivationCppBegin = 24006,
                    perfTailoredWizardAppActivationCppEnd = 24007,
                    perfTailoredWizardAppActivationManagedBegin = 24008,
                    perfTailoredWizardAppActivationManagedEnd = 24009,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeWindowsSimulatorMarkers)

                    
                    perfSimulatorLaunchBegin = 24200,
                    perfSimulatorCompletionRoutineCalled = 24201,
                    perfSimulatorConnectComplete = 24202,
                    perfSimulatorLogonComplete = 24203,
                    perfSimulatorActivateAppCalled = 24204,
                    perfSimulatorConnectStarted = 24205,
                    perfSimulatorLogonStarted = 24206,


#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeGraphicsDebuggerMarkers)

                    

                    perfVSGDEventListLoaded = 24400,
                    perfVSGDPixelHistoryLoaded = 24401,
                    perfVSGDPipelineViewerImageUpdated = 24402,
                    perfVSGDPipelineViewerImageNoData = 24403,
                    perfVSGDCallStackLoaded = 24404,
                    perfVSGDObjectTableLoaded = 24406,
                    perfVSGDFrameBufferLoaded = 24407,
                    perfVSGDLogFileLoaded = 24408,
                    perfPresentIsCalled = 24409,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeReferenceManagerMarkers)

                    

					perfReferenceManagerDisplayedToUser = 24600,

                    perfReferenceManagerPlatformProviderPopulationStarted = 24601,
                    perfReferenceManagerPlatformProviderPopulationFinished = 24602,

					perfReferenceManagerCOMProviderPopulationStarted = 24603,
                    perfReferenceManagerCOMProviderPopulationFinished = 24604,

					perfReferenceManagerConnectedServiceInstanceProviderPopulationStarted = 24605,
                    perfReferenceManagerConnectedServiceInstanceProviderPopulationFinished = 24606,
                    perfAddConnectedServicesTriggered = 24607,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludePageInspectorMarkers)

                    

                    perfPageInspector_ToolWindowReady = 24800,
                    perfPageInspector_NavigationBegin = 24801,
                    perfPageInspector_NavigationEnd = 24802,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeVSLogMarkers)

                    

                    perfVSLog_OnBeforeCloseSolutionBegin = 25000,
                    perfVSLog_OnBeforeCloseSolutionEnd = 25001,
                    perfVSLog_ParseProjectVCTLogBegin = 25002,
                    perfVSLog_ParseProjectVCTLogEnd = 25003,

#endif 

#if (defined Codemarkers_IncludeAllMarkers || defined Codemarkers_IncludeDiagnosticsHubMarkers)

                    

                    perfDiagnosticsHub_StartCollectionBegin = 25200,
                    perfDiagnosticsHub_StartCollectionEnd = 25201,
                    perfDiagnosticsHub_StopCollectionBegin = 25202,
                    perfDiagnosticsHub_StopCollectionEnd = 25203,
                    perfDiagnosticsHub_PackageInitializeBegin = 25204,
                    perfDiagnosticsHub_PackageInitializeEnd = 25205,
                    perfDiagnosticsHub_GraphDataUpdateBegin = 25206,
                    perfDiagnosticsHub_GraphDataUpdateEnd = 25207,
                    perfDiagnosticsHub_StartEtwCollectionBegin = 25208,
                    perfDiagnosticsHub_StartEtwCollectionEnd = 25209,
                    perfDiagnosticsHub_StopEtwCollectionBegin = 25210,
                    perfDiagnosticsHub_StopEtwCollectionEnd = 25211,
                    perfDiagnosticsHub_UserAppLaunched = 25212,

#endif 






                    perfMax,

#ifdef Codemarkers_IncludeAllMarkers
                    
                    perfTimerUser = 32768,
#endif 

            }; 
        } 
    } 
} 

